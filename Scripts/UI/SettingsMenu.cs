using Godot;
using System;

public partial class SettingsMenu : Control
{
	[Export] private TextureButton _muteMusic;
	[Export] private TextureButton _muteSFX;
	[Export] private Texture2D _onTexture;
	[Export] private Texture2D _offTexture;


	public override void _Ready()
	{
		var _musicBus = AudioServer.GetBusIndex("Music");
		if (AudioServer.IsBusMute(_musicBus))
		{
			_muteMusic.TextureNormal = _offTexture;
		}
		else
		{
			_muteMusic.TextureNormal = _onTexture;
		}

		var _sfxBus = AudioServer.GetBusIndex("SFX");
		if (AudioServer.IsBusMute(_sfxBus))
		{
			_muteSFX.TextureNormal = _offTexture;
		}
		else
		{
			_muteSFX.TextureNormal = _onTexture;
		}



	}
	private void OnMuteMusicPressed()
	{
		GD.Print("Mute music Pressed");
		var _musicBus = AudioServer.GetBusIndex("Music");
		AudioServer.SetBusMute(_musicBus, !AudioServer.IsBusMute(_musicBus));

		if (AudioServer.IsBusMute(_musicBus))
		{
			_muteMusic.TextureNormal = _offTexture;
		}
		else
		{
			_muteMusic.TextureNormal = _onTexture;
		}
	}

	private void OnMuteSFXPressed()
	{
		GD.Print("Mute music Pressed");
		var _sfxBus = AudioServer.GetBusIndex("SFX");
		AudioServer.SetBusMute(_sfxBus, !AudioServer.IsBusMute(_sfxBus));

		if (AudioServer.IsBusMute(_sfxBus))
		{
			_muteSFX.TextureNormal = _offTexture;
		}
		else
		{
			_muteSFX.TextureNormal = _onTexture;
		}
	}


    //When button pressed: calls the main menu scene
	private async void OnBackPressed()
	{
		GD.Print("Back Pressed");
		var transition = GetNode<SceneTransition>("/root/SceneTransition");
    	await transition.FadeToBlack();

		GetTree().ChangeSceneToFile("res://Scenes/UI/main_menu.tscn");

		await transition.FadeToNormal();
	}

}
