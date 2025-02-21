using Godot;

namespace snake.scripts;

public partial class SnakeBody : Area2D
{
	// Queue of direction and position, when node gets to item in queue it moves
	// waits until it gets to that position and executes the direction
	// if it's the last node, remove it from the queue
	private SnakeHead SnakeHeadNode { get; set; }
	private int Speed { get; set; }
	private Vector2 Movement { get; set; }
	public int CurrentMove { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SnakeHeadNode = GetNode<SnakeHead>("../SnakeHead");
		// var tes = GetNode<Area2D>("../../Snake").GetChildCount();
		Speed = SnakeHeadNode.Speed;
		Movement = SnakeHeadNode.Movement;
		CurrentMove = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		SnakeHeadNode = GetNode<SnakeHead>("../SnakeHead");

		if (Movement == Vector2.Zero)
		{
			Movement = SnakeHeadNode.Movement;
		}

		if (SnakeHeadNode.MoveQueue.Count > CurrentMove)
		{
			GD.Print($"Move Q Position{SnakeHeadNode.MoveQueue[CurrentMove].GlobalPosition}");
			GD.Print($"Body Position {GlobalPosition}");
			var targetPosition = SnakeHeadNode.MoveQueue[CurrentMove].GlobalPosition;
			
			if (targetPosition.X > 0 && GlobalPosition.X > targetPosition.X)
			{
				SetGlobalPosition(targetPosition);
			}
			else if (targetPosition.X < 0 && GlobalPosition.X < targetPosition.X)
			{
				SetGlobalPosition(targetPosition);
			}
			else if (targetPosition.Y > 0 && GlobalPosition.Y > targetPosition.Y)
			{
				SetGlobalPosition(targetPosition);	
			}
			else if (targetPosition.Y < 0 && GlobalPosition.Y < targetPosition.Y)
			{
				SetGlobalPosition(targetPosition);
			}
			
			GD.Print($"Movement After: {Movement}");
			// move to target position
			
			
			// when you get to the target position, change to the direction.
			// increment move by 1, once the position has been achieved
			if (GlobalPosition == targetPosition)
			{
				Movement = SnakeHeadNode.MoveQueue[CurrentMove].Movement;
				CurrentMove += 1;
			}
		}

		Position += Movement * Speed * (float)delta;
		// var limitX = ScreenSize.X / 2;
		// var limitY = ScreenSize.Y / 2;
		// Position = new Vector2(
		// 	x: Mathf.Clamp(Position.X, limitX * -1, limitX),
		// 	y: Mathf.Clamp(Position.Y, limitY * -1, limitY)
		// );
	}
}
