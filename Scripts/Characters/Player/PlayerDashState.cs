using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimerNode;

    [Export] private float speed = 10;

    public override void _Ready()
    {
        base._Ready();

        dashTimerNode.Timeout += HandleDashTimeout;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
        characterNode.MoveAndSlide();
        characterNode.Flip();
	}

    protected override void EnterState()
    {
        characterNode.animPlayerNode.Play(GameConstants.ANIM_DASH);
        characterNode.Velocity = new(
            characterNode.direction.X, 0, characterNode.direction.Y
        );

            if (characterNode.Velocity == Vector3.Zero)
            {
                characterNode.Velocity = characterNode.spriteNode.FlipH ?
                    Vector3.Left :
                    Vector3.Right;
            }

            characterNode.Velocity *= speed;
            dashTimerNode.Start();
    }

    private void HandleDashTimeout()
    {
        characterNode.Velocity = Vector3.Zero;
        characterNode.stateMachineNode.SwitchStates<PlayerIdleState>();
    }
}
