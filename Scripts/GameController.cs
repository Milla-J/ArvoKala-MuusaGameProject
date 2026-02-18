using Godot;
using System;

public partial class GameController : Node
{
	[ExportCategory("References")]
	[Export] private Minigame _minigame;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void StartMinigame()
	{
		_minigame.StartMinigame();
	}
}
