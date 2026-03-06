using Godot;
using System;

public partial class CreditsMenu : Control
{
	private void OnBackPressed()
	{
		GD.Print("Back Pressed");
		GetTree().ChangeSceneToFile("res://Scenes/UI/main_menu.tscn");

	}

}
