using Godot;
using System;
using System.Collections.Generic;

public partial class GameController : Node
{
	[ExportCategory("References")]
	[Export] private Minigame _minigame;
    [Export] private bool _testBool = false;
    [Export] private Node2D _hook;
    [Export] private Sprite2D _spawnArea;

    [ExportCategory("Fish lists")]
    [Export] private PackedScene[] _fishPool;
    private List<Fish> _spawnedFish = new List<Fish>();
    // the amount of available fish in the spawning pool
    private int _fishPoolCount;
    // index of the fish currently being cought in the game
    private int _currentFishIndex;

    [ExportCategory("UI")]
    [Export] private PauseMenu _pauseMenu;


    private Rect2 _screenRect;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        _fishPoolCount = _fishPool.Length;
        _screenRect = _spawnArea.GetRect();
        SpawnFish(5);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
        if(_testBool)
        {
            _currentFishIndex = GD.RandRange(0, _spawnedFish.Count - 1);
            _spawnedFish[_currentFishIndex].IsTargeting = true;
            _testBool = false;
        }
    }

	public void StartMinigame()
	{
        _minigame.StartMinigame();
	}

    public void WinMinigame()
    {
        _spawnedFish[_currentFishIndex].QueueFree();
        _spawnedFish.RemoveAt(_currentFishIndex);
    }

    public void LoseMinigame()
    {
        _spawnedFish[_currentFishIndex].CanMove = true;
    }

    public void SpawnFish(int fishAmount)
    {
        if (fishAmount > _fishPoolCount)
        {
            GD.PrintErr("Attempted to spawn too many fish");
            return;
        }

        for (int i = 0; i < fishAmount; i++)
        {
            int index = GD.RandRange(0, _fishPoolCount - 1);
            // spawn the fish and add it to the fishies list
            Fish fish = _fishPool[index].Instantiate<Fish>();
            fish.SetGameController(this);
            RemoveFishFromPool(index);

            fish.SetTarget(_hook);
            AddChild(fish);
            _spawnedFish.Add(fish);

            // set the fishes position to a random spot on the screen
            int horizontalPosition = (int)GD.RandRange(_screenRect.Position.X * _spawnArea.Scale.X,
                                                        _screenRect.End.X * _spawnArea.Scale.X);
            int verticalPosition = (int)GD.RandRange((_screenRect.Position.Y * _spawnArea.Scale.Y) + _spawnArea.Position.Y,
                                                        (_screenRect.End.Y * _spawnArea.Scale.Y) + _spawnArea.Position.Y);
            fish.GlobalPosition = new Vector2(horizontalPosition, verticalPosition);
        }
    }

    private void RemoveFishFromPool(int index)
    {
        if (_fishPoolCount <= 0)
        {
            GD.PrintErr("Fish pool empty");
            return;
        }
        _fishPoolCount--;
        PackedScene[] tempArray = new PackedScene[_fishPoolCount];
        int newIndex = 0;
        for (int i = 0; i < _fishPool.Length; i++)
        {
            if (i != index)
            {
                tempArray[newIndex] = _fishPool[i];
                newIndex++;
            }
        }
        _fishPool = tempArray;
    }

    private void OnPausePressed()
	{
		GD.Print("Pause pressed");
        _pauseMenu.Visible = true;
		GetTree().Paused = true;
	}
}
