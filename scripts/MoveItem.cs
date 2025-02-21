using Godot;

namespace snake.scripts;

public class MoveItem (Vector2 movement, Vector2 globalPosition)
{
    public Vector2 Movement { get; set; } = movement;
    
    public Vector2 GlobalPosition { get; set; } = globalPosition;
}
