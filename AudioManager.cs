using Godot;
using System;

public partial class AudioManager : Node
{
	[Export] private AudioStreamPlayer _menuMusic;

	public override void _Ready()
	{
    	PlayMenuMusic();
	}

	public void PlayMenuMusic()
    {
        if (!_menuMusic.Playing)
            _menuMusic.Play();
    }
}
