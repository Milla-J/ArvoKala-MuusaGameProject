using Godot;
using System;

public partial class PauseMenu : Control
{
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
	}

	private void OnSettingsPressed()
	{
		GD.Print("Settings pressed");	
	}

	private void OnQuitPressed()
	{
		GD.Print("Quit pressed");
		GetTree().ChangeSceneToFile("res://Scenes/UI/main_menu.tscn");	
	}
}
