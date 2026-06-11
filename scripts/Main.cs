using System.Collections.Generic;
using Godot;

public partial class Main : Node2D
{
	private Node2D sprites;
	private List<Node2D> spriteNodes = new List<Node2D>();
	private int spritesCount;
	private double elapsed;

	[Export] public Vector2 Center = new Vector2(64, 64);
	[Export] public float Amplitude = 60.0f;
	[Export] public float XCycles = 0.9f;
	[Export] public float YCycles = 0.3f;
	[Export] public float Speed = 1.0f;
	[Export] public float PhaseOffset = 0.2f;

	public override void _Ready()
	{
		sprites = GetNode<Node2D>("Sprites");
		spritesCount = sprites.GetChildCount();

		for (int i = 0; i < spritesCount; i++)
		{
			spriteNodes.Add(sprites.GetChild<Node2D>(i));
		}
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Escape))
			GetTree().Quit();

		elapsed += delta;
		float phase = (float)elapsed * Speed;

		for (int i = 0; i < spriteNodes.Count; ++i)
		{
			float t = phase + i * PhaseOffset;
			float x = Center.X + Amplitude * Mathf.Sin(t * XCycles);
			float y = Center.Y + Amplitude * Mathf.Sin(t * YCycles);
			spriteNodes[i].Position = new Vector2(x, y);
		}
	}
}
