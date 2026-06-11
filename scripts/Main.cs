using Godot;
using System;

public partial class Main : Node2D
{
	private Sprite2D icon;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		icon = GetNode<Sprite2D>("Icon");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		icon.Translate(Vector2.Left);
	}
}
