using Godot;
using System;

public partial class CreditsMenu : Control
{
    //When button pressed: calls the main menu scene
	private async void OnBackPressed()
	{
		GD.Print("Back Pressed");
		GetNode<AudioManager>("/root/AudioManager").UIButton();
		GetNode<AudioManager>("/root/AudioManager").Transition();
		var transition = GetNode<SceneTransition>("/root/SceneTransition");
    	await transition.FadeToBlack();

		GetTree().ChangeSceneToFile("res://Scenes/UI/main_menu.tscn");

		await transition.FadeToNormal();
	}

}
