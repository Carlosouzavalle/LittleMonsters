using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 200f;
	[Export] public float JumpSpeed = 400f;
	[Export] public float Gravity = 1000f;

	private Vector2 velocity = Vector2.Zero;
	private AnimatedSprite2D animatedSprite;

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
    {
        // Gravidade
        if (!IsOnFloor())
            velocity.Y += Gravity * (float)delta;
        else
            velocity.Y = 0;

        // Movimento horizontal
        velocity.X = 0;
        bool moving = false;

        if (Input.IsActionPressed("ui_right"))
        {
            velocity.X += Speed;
            animatedSprite.FlipH = false;
            moving = true;
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            velocity.X -= Speed;
            animatedSprite.FlipH = true;
            moving = true;
        }

        // Pulo
        if (IsOnFloor() && Input.IsActionJustPressed("ui_accept"))
        {
            velocity.Y = -JumpSpeed;
        }

        Velocity = velocity;
        MoveAndSlide();
        velocity = Velocity;

        UpdateAnimation(moving);
    }

	private void UpdateAnimation(bool moving)
	{
		if (!IsOnFloor())
		{
			// Pulando ou caindo
			if (velocity.Y < 0)
				animatedSprite.Play("jump");
			else
				animatedSprite.Play("fall");
		}
		else
		{
			if (moving)
				animatedSprite.Play("run");
			else
				animatedSprite.Play("idle");
		}
	}
}
