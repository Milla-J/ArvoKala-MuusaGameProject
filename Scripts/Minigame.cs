using Godot;
using System;

public partial class Minigame : Sprite2D
{
    [Export] private Sprite2D _indicator;
	private bool _minigameGoing;
    private bool _inSafeArea;

    [Export] private int _winningPointAmount;
    private float _pointCounter;

    // local X coordinate of the right edge of the minigame slider
    private float _rightEdge;
    // local X coordinate of the left edge of the minigame slider
    private float _leftEdge;
    // global X coordinate of the right edge of the minigame slider
    private float _safeAreaRightEdge;
    // global X coordinate of the right edge of the minigame slider
    private float _safeAreaLeftEdge;

    // Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        Rect2 area = GetRect();
        _leftEdge = area.Position.X;
        _rightEdge = area.End.X;

        Sprite2D safeArea = GetChild<Sprite2D>(0);
        _safeAreaLeftEdge = safeArea.GetRect().Position.X * safeArea.Scale.X;
        _safeAreaRightEdge = safeArea.GetRect().End.X * safeArea.Scale.X;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (!_minigameGoing)
        {
            return;
        }

        if (_indicator.GlobalPosition.X > _safeAreaLeftEdge && _indicator.GlobalPosition.X < _safeAreaRightEdge)
        {
            _inSafeArea = true;
        }
        else
        {
            _inSafeArea = false;
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
		_minigameGoing = true;
        GD.Print("Game started");

        GD.Print(_indicator.Position.X);
        GD.Print($"{_safeAreaLeftEdge} : {_safeAreaRightEdge}");
	}

	public void StopMinigame()
	{
		_minigameGoing = false;
        GD.Print("Game ended");
        Visible = false;
	}

    private void MoveIndicator()
    {
        Vector2 movementOffset = new Vector2(Input.GetAccelerometer().X, 0);
        if (movementOffset.X < 0 && _indicator.Position.X > _leftEdge ||
            movementOffset.X > 0 && _indicator.Position.X < _rightEdge)
        {
            _indicator.Translate(movementOffset);
        }
    }

    private void WinMinigame()
    {
        GD.Print("You won!");
        StopMinigame();
    }

    private void LoseMinigame()
    {
        GD.Print("You lost :(");
        StopMinigame();
    }
}
