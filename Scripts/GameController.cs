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
    [Export] private Control _spawnArea;

    [ExportCategory("Fish lists")]
    [Export] private PackedScene[] _fishScenes = new PackedScene[2];

    private Rect2 _screenRect;

    private List<Fish> _fishies = new List<Fish>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        _screenRect = _spawnArea.GetRect();
        SpawnFish(10);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
        if(_testBool)
        {
            _fishies[GD.RandRange(0, _fishies.Count - 1)]._isTargeting = true;
            _testBool = false;
        }
    }

	public void StartMinigame()
	{
		_minigame.Visible = true;
        _minigame.StartMinigame();
	}

    public void SpawnFish(int fishAmount)
    {
        for (int i = 0; i < fishAmount; i++)
        {
            // spawn the fish and add it to the fishies list
            //Fish fish = _fishScene.Instantiate<Fish>();
            Fish fish = _fishScenes[GD.RandRange(0, _fishScenes.Length - 1)].Instantiate<Fish>();
            fish.SetTarget(_hook);
            AddChild(fish);
            _fishies.Add(fish);

            // set the fishes position to a random spot on the screen
            int horizontalPosition = (int)GD.RandRange(_screenRect.Position.X, _screenRect.End.X);
            int verticalPosition = (int)GD.RandRange(_screenRect.Position.Y, _screenRect.End.Y);
            fish.GlobalPosition = new Vector2(horizontalPosition, verticalPosition);
        }
    }
}
