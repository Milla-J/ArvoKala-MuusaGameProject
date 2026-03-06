using Godot;
using System;
using System.IO;

public partial class Minigame : Sprite2D
{
    [Export] private Sprite2D _indicator;
    [Export] private bool _testBool;
	private bool _minigameGoing;

    private float _rightEdge;
    private float _leftEdge;

    // Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        Rect2 area = GetRect();
        _leftEdge = area.Position.X;
        _rightEdge = area.End.X;
        GD.Print($"{_leftEdge} : {_rightEdge}");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 movementOffset = new Vector2(Input.GetAccelerometer().X, 0);
        if (movementOffset.X < 0 && _indicator.Position.X > _leftEdge ||
            movementOffset.X > 0 && _indicator.Position.X < _rightEdge)
        {
            _indicator.Translate(movementOffset);
        }
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
