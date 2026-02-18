using Godot;
using System;

public partial class Fish : RigidBody2D
{
	[ExportCategory("Public veriables")]
	[Export] public string _valueName;
	[Export] public string _valueText;

	[ExportCategory("Movement veriables")]
	[Export] private float _speed = 100; // how fast/far movements are
	[Export] private float _movementDelay = 2.5f; // delay between movements
	/// <summary>
	/// Should always be between 0 and 1
	/// </summary>
	[Export] private float _maximumVerticalAngle = 0.2f;
	[Export] private bool _isTargeting; // whether the fish is swimming towards the hook or just randomly around
	[Export] private Node2D _target; // reference to the target hook
	[Export] private int _stoppingDistanceFromHook = 50; // how many pixels away from the hook does the fish stop moving
	private bool _moving = true; // if the fish is allowed to move or not
	private bool _move = false; // boolean to control movement

	[ExportCategory("Graphics veriables")]
	[Export] private Sprite2D _sprite; // reference to the sprite

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
		if (_moving && _move)
		{
			Vector2 movementDirection;
			if (_isTargeting)
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

		if (GlobalPosition.DistanceTo(_target.GlobalPosition) < 50)
		{
			_moving = false;
		}
	}

	private void Move(Vector2 direction)
	{
		Vector2 movement = direction * _speed;
		ApplyCentralImpulse(movement);

		if (direction.X < 0)
		{
			_sprite.FlipH = false;
			GD.Print("looking left");
		}
		if (direction.X > 0)
		{
			_sprite.FlipH = true;
			GD.Print("looking right");
		}
	}

	private async void Timer(float delay)
	{
		await ToSignal(GetTree().CreateTimer(delay), "timeout");
		_move = true;
	}
}
