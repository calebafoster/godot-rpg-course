using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer animPlayerNode;
    [Export] public Sprite3D spriteNode;
    [Export] public StateMachine stateMachineNode;

    private GameConstants constants = new();

    public Vector2 direction = new();

    public override void _PhysicsProcess(double delta)
    {
        Velocity = new(direction.X, 0, direction.Y);
        Velocity *= 5;

        MoveAndSlide();
        Flip();
    }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
                GameConstants.INPUT_MOVE_LEFT, 
                GameConstants.INPUT_MOVE_RIGHT, 
                GameConstants.INPUT_MOVE_FORWARD, 
                GameConstants.INPUT_MOVE_BACKWARD
        );
    }

    private void Flip()
    {
        bool isMovingHorizontally = Velocity.X != 0;

        if (!isMovingHorizontally) { return; }

        bool isMovingLeft = Velocity.X < 0;
        spriteNode.FlipH = isMovingLeft;
    }
}
