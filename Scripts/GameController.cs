using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public partial class GameController : Node
{
	[ExportCategory("References")]
	[Export] private Minigame _minigame;
    [Export] private Node2D _hook;
    [Export] private Sprite2D _spawnArea;
    [Export] private ValueCloud _cloud;

    [ExportCategory("Fish list")]
    [Export] private PackedScene[] _fishPool;
    private List<Fish> _spawnedFish = new List<Fish>();
    // the amount of available fish in the spawning pool
    private int _fishPoolCount;
    // index of the fish currently being cought in the game
    private int _currentFishIndex;

    [ExportCategory("UI")]
    [Export] private PauseMenu _pauseMenu;
    [Export] private Button _pauseButton;

    [ExportCategory("Misc")]
    [Export] private float _delayBetweenFish;
    private bool _fishTargetingActive = false;
    private bool _gameGoing = true;
    [Export] private TextureRect _fishSpot;
    private int _importantValueCount;
    private ConfigFile _config = new ConfigFile();

    

    private Rect2 _screenRect;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        _fishPoolCount = _fishPool.Length;
        _screenRect = _spawnArea.GetRect();
        SpawnFish(5);
        //Calls the audio manager when scene loads
        GetNode<AudioManager>("/root/AudioManager").PlayGameMusic();
    
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
        if (_spawnedFish.Count == 0 && _gameGoing)
        {
            _gameGoing = false;
            PrintOutFish();
        }

        if(!_fishTargetingActive && _gameGoing)
        {
            FishTimer(_delayBetweenFish);
            _fishTargetingActive = true;
        }
    }

	public void StartMinigame()
	{
        _minigame.StartMinigame();
	}

    public void WinMinigame()
    {
        _cloud.Visible = true;
        _cloud.SetValueDiscription(_spawnedFish[_currentFishIndex].ValueDescription);
        _fishSpot.Texture = _spawnedFish[_currentFishIndex].GetChild<Sprite2D>(0).Texture;
        _spawnedFish[_currentFishIndex].Visible = false;
    }

    public void LoseMinigame()
    {
        _spawnedFish[_currentFishIndex].CanMove = true;
        _fishTargetingActive = false;
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

    public void DespawnFish()
    {
        _spawnedFish[_currentFishIndex].QueueFree();
        _spawnedFish.RemoveAt(_currentFishIndex);
        _fishTargetingActive = false;
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

    private void ActivateFishTrageting()
    {
        _currentFishIndex = GD.RandRange(0, _spawnedFish.Count - 1);
        _spawnedFish[_currentFishIndex].IsTargeting = true;
    }

    private void SaveFishToValues()
    {
        _importantValueCount++;
        _config.SetValue("Fish" + _importantValueCount, "ValueName", _spawnedFish[_currentFishIndex].ValueName);
        _config.SetValue("Fish" + _importantValueCount, "ValueDescription", _spawnedFish[_currentFishIndex].ValueDescription);
        _config.SetValue("Fish" + _importantValueCount, "FishTexture", _spawnedFish[_currentFishIndex].GetChild<Sprite2D>(0).Texture);
        _config.Save("user://valueFish.cfg");

        DespawnFish();
    }

    private void PrintOutFish()
    {
        Error err = _config.Load("user://valueFish.cfg");

        if (err != Error.Ok)
        {
            GD.Print("We done fucked up");
            return;
        }

        foreach (String fish in _config.GetSections())
        {
            var valueName = (String)_config.GetValue(fish, "ValueName");
            var ValueDescription = (String)_config.GetValue(fish, "ValueDescription");
            GD.Print($"{valueName} : {ValueDescription}");
        }
    }

    private void OnPausePressed()
	{
		GD.Print("Pause pressed");
        _pauseMenu.Visible = true;
        _pauseButton.Visible = false;
		GetTree().Paused = true;
        
        GetNode<AudioManager>("/root/AudioManager").SetPausedAudio(true);
	}
    

    private async void FishTimer(float delay)
	{
		await ToSignal(GetTree().CreateTimer(delay), "timeout");
        ActivateFishTrageting();
	}
}
