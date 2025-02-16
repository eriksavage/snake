using Godot;
using System;

public partial class Snake : Area2D

{
	[Export]
	public int Speed { get; set; } = 400;

	public Vector2 ScreenSize;
	public Vector2 Movement;
	public Directions Direction;

	public enum Directions
	{
		Left,
		Right,
		Up,
		Down
	};
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Movement = Vector2.Zero;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// start movement on confirm press
		if (Movement == Vector2.Zero && Input.IsActionPressed("confirm"))
		{
			Direction = Directions.Right;
		}
		
		// Set direction on key press
		if (Input.IsActionPressed("up"))
		{
			Direction = Direction != Directions.Down ? Directions.Up : Directions.Down;	
		}

		if (Input.IsActionPressed("down"))
		{
			Direction = Direction != Directions.Up ? Directions.Down : Directions.Up;
		}

		if (Input.IsActionPressed("left"))
		{
			Direction = Direction != Directions.Right ? Directions.Left : Directions.Right;
		}

		if (Input.IsActionPressed("right"))
		{
			Direction = Direction != Directions.Left ? Directions.Right : Directions.Left;
		}

		// assign movement based on direction
		switch (Direction)
		{
			case Directions.Up:
				Movement.X = 0;
				Movement.Y = -1;
				RotationDegrees = 180;
				break;
			case Directions.Down:
				Movement.X = 0;
				Movement.Y = 1;
				RotationDegrees = 0;
				break;
			case Directions.Left:
				Movement.X = -1;
				Movement.Y = 0;
				RotationDegrees = 90;	
				break;
			case Directions.Right:
				Movement.X = 1;
				Movement.Y = 0;
				RotationDegrees = 270;
				break;
			default:
				Console.WriteLine("Invalid Direction");
				break;
		}
		
		if (Movement.Length() > 0)
		// if (Direction == Directions.Up || Direction == Directions.Down || Direction == Directions.Left || Direction == Directions.Right)
		{
			Movement = Movement.Normalized() * Speed;
			// animatedSprite2D.Play();
		}
		
		// update position of the snake
		Position += Movement * (float)delta;
		var limitX = ScreenSize.X / 2;
		var limitY = ScreenSize.Y / 2;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, limitX * -1, limitX),
			y: Mathf.Clamp(Position.Y, limitY * -1, limitY)
		);
	}
}
