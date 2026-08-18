using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{
	// Called every physics frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
        if (characterNode.direction == Vector2.Zero)
        {
            characterNode.stateMachineNode.SwitchStates<PlayerIdleState>();
            return;
        }

        characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);
        characterNode.Velocity *= 5;

        characterNode.MoveAndSlide();
        characterNode.Flip();
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
        characterNode.animPlayerNode.Play(GameConstants.ANIM_MOVE);
    }
}
