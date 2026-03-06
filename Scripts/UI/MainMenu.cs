using Godot;
using System;

public partial class MainMenu : Control
{
	//When button pressed: calls the game scene
	private void OnPlayPressed()
	{
		GD.Print("Play pressed");
		GetTree().ChangeSceneToFile("res://Scenes/Game.tscn");
	}

	//When button pressed: calls the settings menu scene
	private void OnSettingsPressed()
	{
		GD.Print("Settings pressed");
		GetTree().ChangeSceneToFile("Scenes/UI/settings_menu.tscn");
	}
    //When button pressed: calls the credits menu scene
	private void OnCreditsPressed()
	{
		GD.Print("Credits pressed");
		GetTree().ChangeSceneToFile("res://Scenes/UI/credits_menu.tscn");	
	}

    //When button pressed: quits the game
	private void OnQuitPressed()
	{
		GD.Print("Quit pressed");
		GetTree().Quit();
	}

	

	
}
