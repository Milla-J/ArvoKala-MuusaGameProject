using Godot;
using System;

public partial class Tutorial : Control
{
    [Export] private Control _page1;
    [Export] private Control _page2;

    private void NextTutorialPage()
    {
        _page1.Visible = false;
        _page2.Visible = true;
    }

    private void HideTutorial()
    {
        Visible = false;
    }
}
