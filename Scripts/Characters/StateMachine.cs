using Godot;
using System;

public partial class StateMachine : Node
{

    [Export] private Node currentState;
    [Export] private Node[] states;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        currentState.Notification(5001);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void SwitchStates<T>()
    {
        Node newState = null;

        foreach (Node state in states)
        {
            if (state is T) 
            {
                newState = state;
            }
        }
        if (newState == null) { return; }

        currentState.Notification(5002);
        GD.Print(currentState);
        currentState = newState;
        currentState.Notification(5001);
        GD.Print(currentState);
    }
}
