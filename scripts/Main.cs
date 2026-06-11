using Godot;
using System;

public partial class Main : Node2D
{
	private Node2D black_pixel;
	private Node2D white_pixel;

	private Vector2 black_pixel_default_position;
	private Vector2 white_pixel_default_position;

	private double elapsed = 0.0;
	private float hue = 0.0f;

	private readonly Random rng = new();
	private FastNoiseLite _noise = new();

	[Export]
	public float strength = 8.0f;

	[Export]
	public float timescale = 10.0f;

	[Export(PropertyHint.Range, "0,1,0.01")]
	public float saturation = 1.0f;

	[Export(PropertyHint.Range, "0,1,0.01")]
	public float brightness = 1.0f;

	public override void _Ready()
	{
		black_pixel = GetNode<Node2D>("BlackPixel");
		white_pixel = GetNode<Node2D>("WhitePixel");

		black_pixel_default_position = black_pixel.Position;
		white_pixel_default_position = white_pixel.Position;

		_noise.Seed = (int)GD.Randi();
		_noise.Frequency = 0.1f;
		_noise.FractalOctaves = 1;
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Escape))
		{
			GetTree().Quit();
		}

		elapsed += delta;

		black_pixel.Position = new Vector2(black_pixel_default_position.X + (float)Math.Sin(elapsed) * strength, black_pixel.Position.Y);
		white_pixel.Position = new Vector2(white_pixel_default_position.X + (float)Math.Cos(elapsed) * strength, white_pixel.Position.Y);

		hue = NormalisedPerlin(0.0f, (float)elapsed * timescale);
		white_pixel.Modulate = Color.FromHsv(hue, saturation, brightness);
	}

	private float NormalisedPerlin(float offset, float elapsed)
	{
		float v = _noise.GetNoise1D(offset + elapsed);
		v += 1.0f;
		v /= 2.0f;

		return v;
	}
}
