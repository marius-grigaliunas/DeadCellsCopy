using Godot;
using System;

public partial class Knight : CharacterBody2D
{
	[Export] public AnimatedSprite2D sprite;

	[Export] public float Speed = 150.0f;
	[Export] public float JumpVelocity = -300.0f;


	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;


		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;

		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");

		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			if(direction.X == -1)
			{
				sprite.FlipH = true;
			}
			else
			{
				sprite.FlipH = false;
			}
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

		}

		if(velocity.X > 0)
		{
			sprite.Play("run");
		}
		else
		{
			sprite.Play("idle");
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
