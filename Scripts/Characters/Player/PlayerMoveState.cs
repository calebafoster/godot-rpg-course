using Godot;
using System;

public partial class PlayerMoveState : Node
{
    private Player characterNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        characterNode = GetOwner<Player>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (characterNode.direction == Vector2.Zero)
        {
            characterNode.stateMachineNode.SwitchStates<PlayerIdleState>();
        }
	}

    public override void _Notification(int what) 
    {
        base._Notification(what);

        if (what == 5001)
        {
            characterNode.animPlayerNode.Play(GameConstants.ANIM_MOVE);
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if (what == 5002)
        {
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.stateMachineNode.SwitchStates<PlayerDashState>();
        }
    }
}
