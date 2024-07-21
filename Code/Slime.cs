using Godot;
using System;

public partial class Slime : CharacterBody2D
{

	[Export] AnimatedSprite2D sprite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

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
    }
}
