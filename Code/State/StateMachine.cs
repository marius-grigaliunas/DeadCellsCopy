using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node2D
{
	[Export] public NodePath initialState;
	public Knight player;
	[Export] public Slime slime;
	// [Export] public AnimatedSprite2D sprite;


	private Dictionary<string, State> States;
	private State _currentState = null;
	public string currentState
	{
		get {return _currentState.Name;}
	}	



	// Runs through all the children of the State Machine node and adds all
	// the state nodes to the dictionary of states
	public override void _Ready()
	{
		States = new Dictionary<string, State>();

		foreach (Node node in GetChildren())
		{
			if(node is State state)
			{
				States[node.Name] = state;
				state.stateMachine = this;
				state.Enter();
				state.Exit();
			}
		}


		_currentState = GetNode<State>(initialState);
		_currentState.Enter();
	}


    public override void _Process(double delta)
    {
        _currentState._Process((float) delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        _currentState._PhysicsProcess((float) delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        _currentState.HandleInput(@event);
    }

	public void TransitionTo(string key)
	{
		if(!States.ContainsKey(key) || _currentState == States[key])
			return;

		_currentState.Exit();
		_currentState = States[key];
		_currentState.Enter();
	}
}
