using Godot;
using System;

public partial class CreditsMenu : Control
{
    //When button pressed: calls the main menu scene
	private void OnBackPressed()
	{
		GD.Print("Back Pressed");
		GetTree().ChangeSceneToFile("res://Scenes/UI/main_menu.tscn");
	}

}
