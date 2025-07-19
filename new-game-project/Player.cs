using Godot;
using System;

public partial class CharacterBody2d : CharacterBody2D
{
    public float Speed = 200f;

    [Export]
    public float JumpSpeed = 400f;

    [Export]
    public float Gravity = 1000f;

    private Vector2 velocity = Vector2.Zero;

    public override void _PhysicsProcess(double delta)
    {
        

        if (!IsOnFloor())
        {
            velocity.Y += Gravity * (float)delta;
        }
        else
        {
            velocity.Y = 0; // zera ao encostar no chão
        }

        // Movimento horizontal
        velocity.X = 0;

        if (Input.IsActionPressed("ui_right"))
            velocity.X += Speed;
        if (Input.IsActionPressed("ui_left"))
            velocity.X -= Speed;

        // Pulo
        if (IsOnFloor() && Input.IsActionJustPressed("ui_accept"))
        {
            velocity.Y = -JumpSpeed;
        }

        Velocity = velocity;
        MoveAndSlide();
        velocity = Velocity; // a
    }
}
