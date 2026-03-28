using Godot;
using System;

public partial class ValueProfile : Node2D
{
    [Export] private PackedScene _valueFishScene;
    [Export] private Control _parent;
    private ConfigFile _config = new ConfigFile();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        //PrintSavedFish();
        LoadValueFish();
    }

	private void PrintSavedFish()
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

    private void LoadValueFish()
    {
        Error err = _config.Load("user://valueFish.cfg");

        if (err != Error.Ok)
        {
            GD.Print("We done fucked up");
            return;
        }

        foreach (String fish in _config.GetSections())
        {
            Control valueFish = _valueFishScene.Instantiate<Control>();
            valueFish.GetChild<Label>(0).Text = (String)_config.GetValue(fish, "ValueName");
            valueFish.GetChild<TextureRect>(1).Texture = (Texture2D)_config.GetValue(fish, "FishTexture");
            valueFish.GetChild<Label>(2).Text = (String)_config.GetValue(fish, "ValueDescription");
            _parent.AddChild(valueFish);
        }
    }

    private async void OnExitPressed()
    {
        var transition = GetNode<SceneTransition>("/root/SceneTransition");
    	await transition.FadeToBlack();

        GetTree().ChangeSceneToFile("res://Scenes/UI/main_menu.tscn");

        await transition.FadeToNormal();
    }
}
