using Godot;
using System;

public partial class AudioManager : Node
{
	[Export] private AudioStreamPlayer _menuMusic;
	[Export] private AudioStreamPlayer _mainGameMusic;

	private int _musicBus;
	private int _sfxBus;
    private AudioEffectLowPassFilter _lowPass;
	private AudioEffectLowPassFilter _lowPass2;
	 private AudioEffectReverb _reverb;
	

	public override void _Ready()
	{
    	_musicBus = AudioServer.GetBusIndex("Music");
		_sfxBus = AudioServer.GetBusIndex("SFX");

        _lowPass = (AudioEffectLowPassFilter)AudioServer.GetBusEffect(_musicBus, 0);
		_lowPass2 = (AudioEffectLowPassFilter)AudioServer.GetBusEffect(_sfxBus, 0);
		_reverb = (AudioEffectReverb)AudioServer.GetBusEffect(_musicBus, 1);

        // Start in "normal gameplay" state
        _lowPass.CutoffHz = 20000.0f;
		_lowPass2.CutoffHz = 20000.0f;
		_reverb.Wet = 0.0f;
        AudioServer.SetBusEffectEnabled(_musicBus, 0, true);   // keep LPF on
        AudioServer.SetBusEffectEnabled(_musicBus, 1, true);  //turn reverb on

		AudioServer.SetBusEffectEnabled(_sfxBus, 0, true);   // keep LPF on
	}

	public void PlayMenuMusic()
    {
        if (_mainGameMusic.Playing)
        	_mainGameMusic.Stop();

    	if (!_menuMusic.Playing)
        	_menuMusic.Play();
	}

	public void PlayGameMusic()
	{
		if (_menuMusic.Playing)
			_menuMusic.Stop();

		if (!_mainGameMusic.Playing)
			_mainGameMusic.Play();
	}

	public void SetPausedAudio(bool paused)
	{
		var tween = CreateTween();

		if (paused)
		{
			// Gradually muffle music
			tween.TweenProperty(_lowPass, "cutoff_hz", 1500.0f, 0.3f);

			tween.TweenProperty(_lowPass2, "cutoff_hz", 1500.0f, 0.3f);

			// Gradually increase reverb
			tween.TweenProperty(_reverb, "wet", 0.55f, 0.7f);
		}
		else
		{
			// Raise the frequencies gradually back to normal
			tween.TweenProperty(_lowPass, "cutoff_hz", 20000.0f, 0.7f);

			tween.TweenProperty(_lowPass2, "cutoff_hz", 20000.0f, 0.7f);

			// Fade reverb back out
			tween.TweenProperty(_reverb, "wet", 0.0f, 0.2f);
		}	
		
	}

	public void SetTensionAmount(float amount)
	{
		var syncStream = _mainGameMusic.Stream as AudioStreamSynchronized;

		if (syncStream == null)
		{
			GD.PrintErr("Main game music is not using AudioStreamSynchronized.");
			return;
		}

		amount = Mathf.Clamp(amount, 0f, 1f);
		float volumeDb = Mathf.Lerp(-30.0f, 0.0f, amount);

		syncStream.SetSyncStreamVolume(1, volumeDb);
	}

}
