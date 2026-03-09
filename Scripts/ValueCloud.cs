using Godot;
using System;

public partial class ValueCloud : TextureRect
{
    [Export] private Label _valueDiscription;

    public void SetValueDiscription(string text)
    {
        _valueDiscription.Text = text;
    }

    public void HideCloud()
    {
        Visible = false;
    }
}
