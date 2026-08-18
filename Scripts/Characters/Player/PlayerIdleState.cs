using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
	// Called every physics frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
        if (characterNode.direction != Vector2.Zero)
        {
            characterNode.stateMachineNode.SwitchStates<PlayerMoveState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.stateMachineNode.SwitchStates<PlayerDashState>();
        }
    }

    protected override void EnterState()
    {
        base.EnterState();

        characterNode.animPlayerNode.Play(GameConstants.ANIM_IDLE);
    }
}
