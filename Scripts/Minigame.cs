using Godot;
using System;

public partial class Minigame : Node2D
{
    [ExportCategory("References")]
    [Export] private GameController _gameController;
    [Export] private Area2D _safeArea;
    [Export] private MinigameIndicator _indicator;


    [ExportCategory("Minigame veriables")]
    [Export] private float _tiltThreshold = 5;
    [Export] private int _indicatorSpeedMultiplier = 60;
    [Export] private float _indicatorMaxVelocity = 100f;
    [Export] private int _winningPointAmount;
    private float _pointCounter;
    [Export] private float _safeAreaMovementSpeed = 10;
    private int _safeAreaMovementDirection = 1;
    private bool _firstMinigame = true;
    private bool _waitForPlayerInput = false;
    private bool _minigameGoing;
    private bool _inSafeArea = true;


    public override void _EnterTree()
	{
		_safeArea.BodyEntered += OnSafeAreaEntered;
        _safeArea.BodyExited += OnSafeAreaExit;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (!_minigameGoing)
        {
            return;
        }

        if (_pointCounter >= _winningPointAmount)
        {
            WinMinigame();
        }
        if (_pointCounter <= 0)
        {
            LoseMinigame();
        }
	}

    public override void _PhysicsProcess(double delta)
	{
        if (!_minigameGoing)
        {
            return;
        }

        MoveIndicator();
        MoveSafeArea((float)delta);

        if (_inSafeArea)
        {
            _pointCounter = _pointCounter + (float)(1 * delta);
        }
        else
        {
            _pointCounter = _pointCounter - (float)(1 * delta);
        }
	}

	public void StartMinigame()
	{
        Visible = true;
        _pointCounter = _winningPointAmount / 2f;
        GD.Print(_pointCounter);
        _minigameGoing = true;
        GD.Print("Game started");
	}

    private void OnSafeAreaEntered(Node2D body)
    {
        if (body.IsInGroup("MinigameIndicator"))
        {
            _inSafeArea = true;
        }
        if (body is StaticBody2D)
        {
            _safeAreaMovementDirection = 0 - _safeAreaMovementDirection;
        }
    }

    private void OnSafeAreaExit(Node2D body)
    {
        if (body.IsInGroup("MinigameIndicator"))
        {
            _inSafeArea = false;
        }
    }

    private void MoveIndicator()
    {
        int horizontalDirection = 1;
        if (Input.GetAccelerometer().X < 0)
        {
            horizontalDirection = -1;
        }
        float horizontalVelocity = Mathf.Min(Math.Abs(Input.GetAccelerometer().X) * _indicatorSpeedMultiplier, _indicatorMaxVelocity);
        Vector2 movementOffset = new Vector2(horizontalVelocity * horizontalDirection, 0);
        _indicator.ApplyCentralForce(movementOffset);
    }

    private void MoveSafeArea(float deltaTime)
    {
        Vector2 offset = new Vector2(_safeAreaMovementDirection, 0) * _safeAreaMovementSpeed * deltaTime;
        _safeArea.Translate(offset);
    }

    private void WinMinigame()
    {
        GD.Print("You won!");
        StopMinigame();
        _gameController.WinMinigame();
    }

    private void LoseMinigame()
    {
        GD.Print("You lost :(");
        StopMinigame();
        _gameController.LoseMinigame();
    }

    public void StopMinigame()
	{
		_minigameGoing = false;
        GD.Print("Game ended");
        Visible = false;
        _safeArea.SetPosition(new Vector2());
        _indicator.reset = true;
        _indicator.LinearVelocity = new Vector2();
        _indicator.AngularVelocity = 0;
	}
}
