using Godot;
using System;

public partial class Minigame : Node
{
	private bool _minigameGoing;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_minigameGoing)
		{
			// do the thing
		}
	}

	public void StartMinigame()
	{
		_minigameGoing = true;
	}

	public void StopMinigame()
	{
		_minigameGoing = false;
	}
}
