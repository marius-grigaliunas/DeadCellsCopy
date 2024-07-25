using Godot;
using System;

public partial class Attack : State
{
	Slime enemy;
	[Export] float jumpForce = 100f;
	Knight player;
	[Export] Timer attackTimer;

	bool canAttack;

    public override void Enter()
    {
        player = stateMachine.player;
		enemy = stateMachine.slime;

		if(enemy == null)
		{
			GD.PushError("Enemy refrence for MoveToPlayer state not set");
			GetTree().Quit();
		}

		canAttack = true;
    }

    public override void _PhysicsProcess(double delta)
	  {

	  	if(player != null && stateMachine.currentState == "Attack")
		  {
			  Vector2 jumpDirection = (player.GlobalPosition - enemy.GlobalPosition).Normalized();

			  Vector2 velocity = enemy.Velocity;

			if(enemy.IsOnFloor() && canAttack)
			{
				canAttack = false;
				attackTimer.Start();
				velocity.X = jumpDirection.X * jumpForce;
				velocity.Y = jumpDirection.Y * jumpForce*2;
			}

			enemy.Velocity = velocity;
			enemy.sprite.Play("attack");

			enemy.MoveAndSlide();			

			
			// GD.Print("Attack. Distance: " + DistanceToPlayer());
			
			if(DistanceToPlayer() > 60f)
			{
				stateMachine.TransitionTo("MoveToPlayer");
			}
		}

	}

	public float DistanceToPlayer()
	{
		return enemy.GlobalPosition.DistanceTo(player.GlobalPosition);
	}

	public void OnAttackTimerTimeout() 
	{
		canAttack = true;
		GD.Print("Can attack");
	}
}
