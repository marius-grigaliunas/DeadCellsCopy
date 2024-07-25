using Godot;
using System;

public partial class MoveToPlayer : State
{
	Slime enemy;
	[Export] float movingSpeed = 70f;
	public Knight player;
	Vector2 moveDirection;

	public override void Enter()
	{
		player = stateMachine.player;
		enemy = stateMachine.slime;

		if(enemy == null)
		{
			GD.PushError("Enemy refrence for MoveToPlayer state not set");
			GetTree().Quit();
		}

	}

	public override void _PhysicsProcess(double delta)
	{	
		if(stateMachine.currentState == "MoveToPlayer")
		{
			DirectionToPlayer();
			Vector2 velocity = enemy.Velocity;

			velocity.X = moveDirection.X * movingSpeed;	

			enemy.Velocity = velocity;
			enemy.MoveAndSlide();
			enemy.MovingAnimationController(velocity);
			
			///--------
			// Transition to attack logic

			if(enemy.GlobalPosition.DistanceTo(player.GlobalPosition) < 60f)
			{
				moveDirection = Vector2.Zero;
				enemy.Velocity = Vector2.Zero;

				stateMachine.TransitionTo("Attack");
			}


			// GD.Print("MoveTo. Distance: " + enemy.GlobalPosition.DistanceTo(player.GlobalPosition));
		}
	}

	public void DirectionToPlayer()
	{

		moveDirection = (player.GlobalPosition - enemy.GlobalPosition).Normalized();
	}

	public void OnPlayerCheckBodyExited(Node2D node)
	{
		if(node.GetType() == typeof(Knight))
		{
			moveDirection = Vector2.Zero; 
			enemy.Velocity = Vector2.Zero;

			stateMachine.player = null;
			player = null;

			GD.Print("BodyExited");
			
			stateMachine.TransitionTo("Idle");
		}
	}


}
