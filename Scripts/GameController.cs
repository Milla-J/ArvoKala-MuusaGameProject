using Godot;
using System;
using System.Collections.Generic;

public partial class GameController : Node
{
	[ExportCategory("References")]
	[Export] private Minigame _minigame;
    [Export] private PackedScene _fishScene;
    [Export] private bool _testBool = false;
    [Export] private Node2D _hook;

    private List<Fish> _fishies = new List<Fish>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        SpawnFish();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
        if(_testBool)
        {
            SpawnFish();
            _testBool = false;
        }
    }

	public void StartMinigame()
	{
		_minigame.Visible = true;
        _minigame.StartMinigame();
	}

    public void SpawnFish()
    {
        Fish fish = _fishScene.Instantiate<Fish>();
        fish.SetTarget(_hook);
        AddChild(fish);
        _fishies.Add(fish);
    }
}
