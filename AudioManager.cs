using Godot;
using System;

public partial class AudioManager : Node
{
	[Export] private AudioStreamPlayer _menuMusic;
	[Export] private AudioStreamPlayer _mainGameMusic;

	public override void _Ready()
	{
    	
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
}
