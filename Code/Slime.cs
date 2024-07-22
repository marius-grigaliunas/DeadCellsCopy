using Godot;
using System;

public partial class Slime : CharacterBody2D
{

	[Export] AnimatedSprite2D sprite;

	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


	//Create a switch thqt connects the states from the state manager to the animations
    public override void _PhysicsProcess(double delta)
    {
		Vector2 velocity = Velocity;

		if(velocity.X == 0)
		{
			sprite.Play("idle");
		}
        else if(velocity.X > 0)
		{
			sprite.Play("moving");
			sprite.FlipH = false;
		}
		else if(velocity.X < 0)
		{
			sprite.Play("moving");
			sprite.FlipH = true;
		}

		if(!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}

		Velocity = velocity;
		MoveAndSlide();
    }
}
