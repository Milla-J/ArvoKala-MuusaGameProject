using Godot;
using System;

public partial class Minigame : Sprite2D
{
    [Export] private Sprite2D _indicator;
	private bool _minigameGoing;
    private bool _inSafeArea;

    private float _rightEdge;
    private float _leftEdge;
    private float _safeAreaRightEdge;
    private float _safeAreaLeftEdge;

    // Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        Rect2 area = GetRect();
        _leftEdge = area.Position.X;
        _rightEdge = area.End.X;

        Rect2 safeArea = GetChild<Sprite2D>(0).GetRect();
        _safeAreaLeftEdge = safeArea.Position.X;
        _safeAreaRightEdge = safeArea.End.X;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (_indicator.Position.X > _safeAreaLeftEdge && _indicator.Position.X < _safeAreaRightEdge)
        {
            _inSafeArea = true;
        }
        else
        {
            _inSafeArea = false;
        }
	}

    public override void _PhysicsProcess(double delta)
	{
		MoveIndicator();

        if (_inSafeArea)
        {

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

    private void MoveIndicator()
    {
        Vector2 movementOffset = new Vector2(Input.GetAccelerometer().X, 0);
        if (movementOffset.X < 0 && _indicator.Position.X > _leftEdge ||
            movementOffset.X > 0 && _indicator.Position.X < _rightEdge)
        {
            _indicator.Translate(movementOffset);
        }
    }
}
