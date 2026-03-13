using Godot;
using System;

public partial class AudioManager : Node
{
	[Export] private AudioStreamPlayer _menuMusic;
	[Export] private AudioStreamPlayer _mainGameMusic;

	private int _musicBus;
    private AudioEffectLowPassFilter _lowPass;
	 private AudioEffectReverb _reverb;
	

	public override void _Ready()
	{
    	_musicBus = AudioServer.GetBusIndex("Music");

        _lowPass = (AudioEffectLowPassFilter)AudioServer.GetBusEffect(_musicBus, 0);
		_reverb = (AudioEffectReverb)AudioServer.GetBusEffect(_musicBus, 1);

        // Start in "normal gameplay" state
        _lowPass.CutoffHz = 20000.0f;
        AudioServer.SetBusEffectEnabled(_musicBus, 0, true);   // keep LPF on
        AudioServer.SetBusEffectEnabled(_musicBus, 1, true);  //turn reverb on
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
			tween.TweenProperty(_lowPass, "cutoff_hz", 700.0f, 0.3f);

			// Gradually increase reverb
			tween.TweenProperty(_reverb, "wet", 0.55f, 0.7f);
		}
		else
		{
			// Restore normal sound
			tween.TweenProperty(_lowPass, "cutoff_hz", 20000.0f, 0.7f);

			// Fade reverb back out
			tween.TweenProperty(_reverb, "wet", 0.0f, 0.2f);
		}	
		
	}
}
