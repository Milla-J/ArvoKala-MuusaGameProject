using Godot;
using System;

public partial class SettingsMenu : Control
{
	private void OnBackPressed()
	{
		GD.Print("Back Pressed");
		GetTree().ChangeSceneToFile("res://Scenes/UI/main_menu.tscn");
	}

}
