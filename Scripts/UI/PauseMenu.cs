using Godot;
using System;

public partial class PauseMenu : Control
{
	[Export] private Button _pauseButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 Visible = false;
	}

	private void OnResumePressed()
	{
		GD.Print("Resume pressed");
		GetTree().Paused = false;
		Visible = false;
		_pauseButton.Visible = true;

		GetNode<AudioManager>("/root/AudioManager").SetPausedAudio(false);
	}

	private void OnSettingsPressed()
	{
		GD.Print("Settings pressed");	
	}

	private async void OnQuitPressed()
	{
		GD.Print("Quit pressed");
		GetTree().Paused = false;
		GetNode<AudioManager>("/root/AudioManager").SetPausedAudio(false);

		var transition = GetNode<SceneTransition>("/root/SceneTransition");
    	await transition.FadeToBlack();	

		GetTree().ChangeSceneToFile("res://Scenes/UI/main_menu.tscn");

		await transition.FadeToNormal();
		
	}
}
