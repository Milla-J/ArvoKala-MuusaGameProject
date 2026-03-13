using Godot;
using System;

public partial class MinigameIndicator : RigidBody2D
{
    public bool reset = false;
    private Transform2D defaultTransform;

    public override void _Ready()
    {
        defaultTransform = GetGlobalTransform();
    }

	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        if (reset)
        {
            state.SetTransform(defaultTransform);
            reset = false;
        }
    }
}
