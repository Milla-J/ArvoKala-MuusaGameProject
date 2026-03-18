using Godot;
using System;

public partial class SettingsMenu : Control
{
	[Export] private Button _muteMusic;
	[Export] private Button _muteSFX;
	

	private void OnMuteMusicPressed()
	{
		GD.Print("Mute music Pressed");
		var _musicBus = AudioServer.GetBusIndex("Music");
		AudioServer.SetBusMute(_musicBus, !AudioServer.IsBusMute(_musicBus));

		if (AudioServer.IsBusMute(_musicBus))
		{
			_muteMusic.Text = "OFF";
		}
		else
		{
			_muteMusic.Text = "ON";	
		}
	}

	private void OnMuteSFXPressed()
	{
		GD.Print("Mute music Pressed");
		var _sfxBus = AudioServer.GetBusIndex("SFX");
		AudioServer.SetBusMute(_sfxBus, !AudioServer.IsBusMute(_sfxBus));

		if (AudioServer.IsBusMute(_sfxBus))
		{
			_muteSFX.Text = "OFF";
		}
		else
		{
			_muteSFX.Text = "ON";	
		}
	}


    //When button pressed: calls the main menu scene
	private void OnBackPressed()
	{
		GD.Print("Back Pressed");
		GetTree().ChangeSceneToFile("res://Scenes/UI/main_menu.tscn");
	}

}
