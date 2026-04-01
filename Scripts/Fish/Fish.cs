using Godot;
using System;

public partial class Fish : RigidBody2D
{
	[ExportCategory("Public veriables")]
	[Export] public string ValueName;
	[Export (PropertyHint.MultilineText)] public string ValueDescription;

	[ExportCategory("Movement veriables")]
	[Export] private float _speed = 150; // how fast/far movements are
	[Export] private float _movementDelay = 2.5f; // delay between movements
	[Export] private float _maximumVerticalAngle = 0.2f; // should be between 0 and 1
	[Export] public bool IsTargeting; // whether the fish is swimming towards the hook or just randomly around
	[Export] private Node2D _target; // reference to the target hook
	[Export] private int _stoppingDistanceFromHook = 50; // how many pixels away from the hook does the fish stop moving
	public bool CanMove = true; // if the fish is allowed to move or not
	private bool _move = false; // boolean to control movement

	[ExportCategory("Graphics veriables")]
	[Export] private Sprite2D _sprite; // reference to the sprite

    //Game controller and minigame veriables
    private GameController _gameController;

    public void SetGameController(GameController gameController)
    {
        _gameController = gameController;
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// assign a random starting offset so fish don't all move at the same time
		float offset = (float)GD.RandRange(0, _movementDelay);
		Timer(offset);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (CanMove && _move)
		{
			Vector2 movementDirection;
			if (IsTargeting)
			{
				movementDirection = (_target.GlobalPosition - GlobalPosition).Normalized();
				GD.Print(GlobalPosition.DistanceTo(_target.GlobalPosition));
			}
			else
			{
				float horizontalDirection = (float)GD.RandRange(-1.0, 1.0);
				float vericalDirection = (float)GD.RandRange(0 - _maximumVerticalAngle, _maximumVerticalAngle);
				movementDirection = new Vector2(horizontalDirection, vericalDirection);

			}
			Move(movementDirection);
			_move = false;
			Timer(_movementDelay);
		}

		if (GlobalPosition.DistanceTo(_target.GlobalPosition) < 50 && IsTargeting)
		{
			CanMove = false;
            _gameController.StartMinigame();
            IsTargeting = false;
		}
	}

	private void Move(Vector2 direction)
	{
		Vector2 movement = direction * _speed;
		ApplyCentralImpulse(movement);

		if (direction.X < 0)
		{
			_sprite.FlipH = false;
		}
		if (direction.X > 0)
		{
			_sprite.FlipH = true;
		}
	}

    public void SetPositioAndRotation(Vector2 position, float rotation, bool flipped)
    {
        Position = position;
        //Rotation = rotation;
        SetRotationDegrees(rotation);
        _sprite.FlipH = flipped;
    }

    public void SetPositioAndRotation(float rotation)
    {
        Rotation = rotation;
    }

	private async void Timer(float delay)
	{
		await ToSignal(GetTree().CreateTimer(delay), "timeout");
		_move = true;
	}

    public void SetTarget(Node2D target)
    {
        _target = target;
    }
}
