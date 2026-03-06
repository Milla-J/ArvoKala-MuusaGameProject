using Godot;
using System;

public partial class Minigame : Node2D
{
    [Export] private Sprite2D _indicator;
    [Export] private bool _testBool;
	private bool _minigameGoing;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 movementOffset = new Vector2(Input.GetAccelerometer().X, 0);
        _indicator.Translate(movementOffset);
	}

	public void StartMinigame()
	{
		_minigameGoing = true;
        GD.Print("Game strated");
	}

	public void StopMinigame()
	{
		_minigameGoing = false;
        GD.Print("Game ended");
        Visible = false;
	}
}
