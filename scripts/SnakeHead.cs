using Godot;
using System.Collections.Generic;

namespace snake.scripts;

public partial class SnakeHead : Area2D

{
	[Export]
	public int Speed { get; set; } = 300;

	public Vector2 ScreenSize;
	public Vector2 Movement;
	public Directions Direction;
	public List<MoveItem> MoveQueue;

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
		MoveQueue = new List<MoveItem>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// start movement on confirm press
		if (Movement == Vector2.Zero && Input.IsActionPressed("confirm"))
		{
			Movement = Vector2.Right;
			RotationDegrees = 270;
			Direction = Directions.Right;
		}
		
		// Set direction on key press
		if (Movement != Vector2.Zero)
		{
			if (Input.IsActionPressed("up"))
        	{
        		if (Direction != Directions.Down && Direction != Directions.Up)
        		{
        			Movement = Vector2.Up;
        			RotationDegrees = 180;
        			Direction = Directions.Up;
        			MoveQueue.Add(new MoveItem(Movement, GlobalPosition));
			        // GD.Print(MoveQueue.Count);
        		}
        	}
    
        	if (Input.IsActionPressed("down"))
        	{
        		if (Direction != Directions.Down && Direction != Directions.Up)
        		{
        			Movement = Vector2.Down;
        			RotationDegrees = 0;
        			Direction = Directions.Down;
        			MoveQueue.Add(new MoveItem(Movement, GlobalPosition));
			        // GD.Print(MoveQueue.Count);
        		}
        	}
    
        	if (Input.IsActionPressed("left"))
        	{
        		if (Direction != Directions.Left && Direction != Directions.Right)
                {
                    Movement = Vector2.Left;
                    RotationDegrees = 90;
                    Direction = Directions.Left;
                    MoveQueue.Add(new MoveItem(Movement, GlobalPosition));
                    // GD.Print(MoveQueue.Count);
                }
        	}
    
        	if (Input.IsActionPressed("right"))
        	{
        		if (Direction != Directions.Left && Direction != Directions.Right)
        		{
        			Movement = Vector2.Right;
        			RotationDegrees = 270;
        			Direction = Directions.Right;
        			MoveQueue.Add(new MoveItem(Movement, GlobalPosition));
					// GD.Print(MoveQueue.Count);
        		}
        	}	
		}

		// if (Movement.Length() > 0)
		// {
			// Movement = Movement.Normalized() * Speed;
			// animatedSprite2D.Play();
		// }
		
		// update position of the snake
		Position += Movement * Speed * (float)delta;
		var limitX = ScreenSize.X / 2;
		var limitY = ScreenSize.Y / 2;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, limitX * -1, limitX),
			y: Mathf.Clamp(Position.Y, limitY * -1, limitY)
		);
	}
}
