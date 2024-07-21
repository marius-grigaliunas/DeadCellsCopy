using Godot;
using System;

public partial class KillLine : Area2D
{
	Timer timer;

    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
    }

    private void OnBodyEntered(Node2D node)
	{
		GD.Print("DEAD, implement Dead screen menu");
		timer.Start();
	}	

	private void _on_timer_timeout() 
	{
		GetTree().ReloadCurrentScene();
	}

}
