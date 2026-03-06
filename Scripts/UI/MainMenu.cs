using Godot;
using System;

public partial class MainMenu : Control
{
	
	public override void _Ready()
	{	
	}
	private void OnPlayPressed()
	{
		GD.Print("Play pressed");
		GetTree().ChangeSceneToFile("res://Scenes/Game.tscn");

	}

	
	private void OnSettingsPressed()
	{
		GD.Print("Settings pressed");
		GetTree().ChangeSceneToFile("res://Scenes/UI/SettingsMenu.tscn");

	}

	private void OnCreditsPressed()
	{
		GD.Print("Credits pressed");
		GetTree().ChangeSceneToFile("res://Scenes/UI/CreditsMenu.tscn");
		
		
	}

	private void OnQuitPressed()
	{
		GD.Print("Quit pressed");
		GetTree().Quit();
	}

	

	
}
