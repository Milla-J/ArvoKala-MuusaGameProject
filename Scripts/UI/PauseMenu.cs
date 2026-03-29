using Godot;
using System;

public partial class PauseMenu : Control
{
	[Export] private TextureButton _muteMusic;
	[Export] private TextureButton _muteSFX;
	[Export] private Texture2D _onTexture;
	[Export] private Texture2D _offTexture;

	[Export] private Button _pauseButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 Visible = false;

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

	private void OnResumePressed()
	{
		GD.Print("Resume pressed");
		GetTree().Paused = false;
		Visible = false;
		_pauseButton.Visible = true;

		GetNode<AudioManager>("/root/AudioManager").SetPausedAudio(false);
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


	private async void OnQuitPressed()
	{
		GD.Print("Quit pressed");
		GetTree().Paused = false;
		GetNode<AudioManager>("/root/AudioManager").SetPausedAudio(false);

		var transition = GetNode<SceneTransition>("/root/SceneTransition");
    	await transition.FadeToBlack();	

		GetNode<AudioManager>("/root/AudioManager").StopReelingAndSplashing();

		GetTree().ChangeSceneToFile("res://Scenes/UI/main_menu.tscn");

		await transition.FadeToNormal();
		
	}
}
