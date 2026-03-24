using Godot;
using System.Threading.Tasks;

public partial class SceneTransition : Control
{
	[Export] private ColorRect _colorRect;
	[Export] private AnimationPlayer _animationPlayer;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible = true;
	}

	public async Task FadeToBlack()
    {
        _animationPlayer.Play("FadeToBlack");
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
    }

	public async Task FadeToNormal()
    {
        _animationPlayer.Play("FadeToNormal");
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
    }

	public async Task FadeToNormalLong()
    {
        _animationPlayer.Play("FadeToNormalLong");
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
    }

	
}
