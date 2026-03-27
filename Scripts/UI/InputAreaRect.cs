using Godot;
using System;

public partial class InputAreaRect : ColorRect
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        MouseEntered += OnMouseEnter;
        MouseExited += OnMouseExit;
    }

    public bool IsMouseEntered
    {
        get;
        private set;
    } = false;

    private void OnMouseEnter()
    {
        IsMouseEntered = true;
    }

    private void OnMouseExit()
    {
        IsMouseEntered = false;
    }
}
