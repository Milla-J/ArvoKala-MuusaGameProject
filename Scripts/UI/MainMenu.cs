using Godot;
using System;

public partial class MainMenu : Control
{
	public override void _Ready()
	{
    	GetNode<AudioManager>("/root/AudioManager").PlayMenuMusic();
	}

	//When button pressed: calls the game scene
	private async void OnPlayPressed()
	{
		GD.Print("Play pressed");
		GetNode<AudioManager>("/root/AudioManager").UIButton();

		GetNode<AudioManager>("/root/AudioManager").StopMenuMusic();
		GetNode<AudioManager>("/root/AudioManager").PlayStartGame();
		var transition = GetNode<SceneTransition>("/root/SceneTransition");
    	await transition.FadeToBlackLong2();

		GetTree().ChangeSceneToFile("res://Scenes/Game.tscn");

		await transition.FadeToNormalLong();
	}

	//When button pressed: calls the settings menu scene
	private async void OnSettingsPressed()
	{
		GD.Print("Settings pressed");
		GetNode<AudioManager>("/root/AudioManager").UIButton();
		GetNode<AudioManager>("/root/AudioManager").Transition();
		var transition = GetNode<SceneTransition>("/root/SceneTransition");
    	await transition.FadeToBlack();

		GetTree().ChangeSceneToFile("Scenes/UI/settings_menu.tscn");

		await transition.FadeToNormal();
	}

    //When button pressed: calls the credits menu scene
	private async void OnCreditsPressed()
	{
		GD.Print("Credits pressed");
		GetNode<AudioManager>("/root/AudioManager").UIButton();
		GetNode<AudioManager>("/root/AudioManager").Transition();
		var transition = GetNode<SceneTransition>("/root/SceneTransition");
    	await transition.FadeToBlack();

		GetTree().ChangeSceneToFile("res://Scenes/UI/credits_menu.tscn");

		await transition.FadeToNormal();
	}

    //When button pressed: quits the game
	private async void OnQuitPressed()
	{
		GD.Print("Quit pressed");
		GetNode<AudioManager>("/root/AudioManager").UIButton();
		GetNode<AudioManager>("/root/AudioManager").Transition();
		var transition = GetNode<SceneTransition>("/root/SceneTransition");
    	await transition.FadeToBlack();

		GetTree().Quit();
	}

	private void OnFiPressed()
    {
		GetNode<AudioManager>("/root/AudioManager").UIButton();
        TranslationServer.SetLocale("fi");
    }

    private void OnEnPressed()
    {
		GetNode<AudioManager>("/root/AudioManager").UIButton();
        TranslationServer.SetLocale("en");
    }
}
