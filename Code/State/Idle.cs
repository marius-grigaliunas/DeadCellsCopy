using Godot;
using System;

public partial class Idle : State
{
	[Export] CharacterBody2D enemy;
	[Export] float movingSpeed = 20f;

	Vector2 moveDirection;
	float wanderTime;

	public void RandomizeWander() 
	{
		moveDirection = new Vector2((float)GD.RandRange(-1, 1), 0).Normalized();
		wanderTime = (float)GD.RandRange(3, 5);

	}

    public override void Enter()
    {
		GD.Print("Idle entered");
        RandomizeWander();
    }

    public override void _Process(double delta)
    {
        if(wanderTime > 0)
		{
			wanderTime -= (float)delta;
		}
		else
		{
			RandomizeWander();
		}
    }

    public override void _PhysicsProcess(double delta)
    {
		if(stateMachine.currentState == "Idle")
		{
			Vector2 velocity = enemy.Velocity;

			if(enemy != null)
			{
				velocity.X = moveDirection.X * movingSpeed;
			}

			enemy.Velocity = velocity;
			enemy.MoveAndSlide();
		}
    }

	public void OnPlayerCheckBodyEntered(Node2D node)
	{
		if(node.GetType() == typeof(Knight))
		{
			wanderTime = 0;
			moveDirection = Vector2.Zero;
			enemy.Velocity = Vector2.Zero;
			
			stateMachine.player = (Knight)node;
            GD.Print("Body entered");

			stateMachine.TransitionTo("MoveToPlayer");
		}
	}
}
