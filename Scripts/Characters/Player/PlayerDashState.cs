using Godot;
using System;

public partial class PlayerDashState : Node
{
    private Player characterNode;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        characterNode = GetOwner<Player>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == 5001)
        {
            characterNode.animPlayerNode.Play(GameConstants.ANIM_DASH);
        }
    }
}
