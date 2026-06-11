using Godot;

public partial class Main : Node2D
{
	private Node2D blackPixel;
	private Node2D whitePixel;

	private Vector2 blackPixelStart;
	private Vector2 whitePixelStart;

	private double elapsed;
	private readonly FastNoiseLite noise = new();

	[Export] public float strength = 56.0f;
	[Export] public float timescale = 4.0f;
	[Export(PropertyHint.Range, "0,1,0.01")] public float saturation = 1.0f;
	[Export(PropertyHint.Range, "0,1,0.01")] public float brightness = 1.0f;

	public override void _Ready()
	{
		blackPixel = GetNode<Node2D>("BlackPixel");
		whitePixel = GetNode<Node2D>("WhitePixel");

		blackPixelStart = blackPixel.Position;
		whitePixelStart = whitePixel.Position;

		noise.Seed = (int)GD.Randi();
		noise.Frequency = 0.1f;
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Escape))
			GetTree().Quit();

		elapsed += delta;
		float t = (float)elapsed;

		Oscillate(blackPixel, blackPixelStart, Mathf.Sin(t));
		Oscillate(whitePixel, whitePixelStart, Mathf.Cos(t));

		float hue = NormalisedNoise(0.0f, t * timescale);
		whitePixel.Modulate = Color.FromHsv(hue, saturation, brightness);
	}

	private void Oscillate(Node2D node, Vector2 origin, float wave)
	{
		node.Position = new Vector2(origin.X + wave * strength, node.Position.Y);
	}

	private float NormalisedNoise(float offset, float elapsed)
	{
		float v = noise.GetNoise1D(offset + elapsed);
		return (v + 1.0f) / 2.0f;
	}
}
