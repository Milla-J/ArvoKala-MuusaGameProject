using Godot;
using System;

public partial class Minigame : Node2D
{
    [Export] private bool _testBool;
	private bool _minigameGoing;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_minigameGoing)
		{
			if (_testBool)
            {
                StopMinigame();
            }
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
