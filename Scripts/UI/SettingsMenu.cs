using Godot;
using System;

public partial class SettingsMenu : Control
{

	private void OnMuteMusicPressed()
	{
		GD.Print("Mute music Pressed");
		var _musicBus = AudioServer.GetBusIndex("Music");
		AudioServer.SetBusMute(_musicBus, true);
	}

	private void OnMuteSFXPressed()
	{
		GD.Print("Mute music Pressed");
		var _sfxBus = AudioServer.GetBusIndex("Music");
		AudioServer.SetBusMute(_sfxBus, true);
	}


    //When button pressed: calls the main menu scene
	private void OnBackPressed()
	{
		GD.Print("Back Pressed");
		GetTree().ChangeSceneToFile("res://Scenes/UI/main_menu.tscn");
	}

}
