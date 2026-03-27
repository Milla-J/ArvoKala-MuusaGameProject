using Godot;
using System;

public partial class PromptPlayerInput : Control
{
    private AnimationPlayer _anim;

    public override void _Ready()
    {
        _anim = GetChild<AnimationPlayer>(0);
    }

	public void Appear()
    {
        _anim.Play("Appear");
    }

    public void Disappear()
    {
        _anim.Play("RESET");
    }
}
