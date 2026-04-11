using Godot;
using System;

public partial class Tutorial : Control
{
    [Export] private Control _page1;
    [Export] private Control _page2;

    private void NextTutorialPage()
    {
        GetNode<AudioManager>("/root/AudioManager").UIButton();
        _page1.Visible = false;
        _page2.Visible = true;
    }

    private void HideTutorial()
    {
        GetNode<AudioManager>("/root/AudioManager").UIButton();
        Visible = false;
    }
}
