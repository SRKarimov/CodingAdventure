using System.Numerics;
using Raylib_cs;

namespace SimpleRuleLife;

class SimpleRuleLife {
    private const int Width = 1920;
    private const int Height = 1080;
    private static readonly List<Particle> Particles = [];
    private static List<Particle> _yellow = [];
    private static List<Particle> _red = [];
    private static List<Particle> _green = [];
    private static List<Particle> _white = [];

    private static readonly Random Random = new();

    [STAThread]
    public static void Main() {
        Raylib.InitWindow(Width, Height, "Life from a Simple Rules");

        Init();

        while (!Raylib.WindowShouldClose()) {
            // Input();
            Update();
            Draw();
        }

        Raylib.CloseWindow();
    }

    private static void Init() {
        _yellow = CreateGroup(1000, Color.Yellow);
        _red = CreateGroup(1000, Color.Red);
        _green = CreateGroup(1000, Color.Green);
        // _white = CreateGroup(1000, Color.White);
        Particles.AddRange(_yellow);
        Particles.AddRange(_red);
        Particles.AddRange(_green);
        // Particles.AddRange(_white);
    }

    private static void Update() {
        Rule(ref _green, ref _green, -0.32f);
        Rule(ref _green, ref _green, -0.17f);
        Rule(ref _green, ref _green, 0.34f);
        Rule(ref _red, ref _green, -0.1f);
        Rule(ref _red, ref _green, -0.34f);
        Rule(ref _yellow, ref _green, 0.15f);
        Rule(ref _yellow, ref _green, -0.2f);
        // Rule(ref _green, ref _green, -0.32f);
        // Rule(ref _green, ref _red, -0.17f);
        // Rule(ref _green, ref _yellow, 0.34f);
        // Rule(ref _red, ref _red, -0.1f);
        // Rule(ref _red, ref _green, -0.34f);
        // Rule(ref _yellow, ref _yellow, 0.15f);
        // Rule(ref _yellow, ref _green, -0.20f);
        // Rule(ref _yellow, ref _red, -0.15f);
        // Rule(ref _red, ref _green, 0.15f);
        // Rule(ref _red, ref _white, 0.15f);
        // Rule(ref _white, ref _green, 0.15f);
    }

    private static void Rule(ref List<Particle> first, ref List<Particle> second, float gravity) {
        foreach (var a in first) {
            var fx = 0.0;
            var fy = 0.0;

            foreach (var b in second) {
                var dx = a.X - b.X;
                var dy = a.Y - b.Y;

                var distance = Math.Sqrt(dx * dx + dy * dy);
                if (distance is <= 0 or >= 80) continue;

                var F = gravity / distance;
                fx += F * dx;
                fy += F * dy;
            }

            a.VX = (float)((a.VX + fx) * 0.5);
            a.VY = (float)((a.VY + fy) * 0.5);

            a.X += a.VX;
            a.Y += a.VY;

            switch (a.X) {
                case <= 0:
                    a.VX *= -1;
                    a.X = 0;
                    break;
                case >= Width:
                    a.VX *= -1;
                    a.X = Width - a.Size;
                    break;
            }

            switch (a.Y) {
                case <= 0:
                    a.VY *= -1;
                    a.Y = 0;
                    break;
                case >= Height:
                    a.VY *= -1;
                    a.Y = Height - a.Size;
                    break;
            }
        }
    }

    private static List<Particle> CreateGroup(int number, Color color) {
        var group = new List<Particle>();
        for (var i = 0; i < number; i++) {
            group.Add(new Particle(RandomFloat(0, Width), RandomFloat(0, Height), 0, 0, color));
        }

        return group;
    }

    private static void Draw() {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        foreach (var particle in Particles) {
            DrawRectangle(particle.X, particle.Y, particle.Size, particle.Color);
        }

        Raylib.DrawFPS(20, Height - 30);
        Raylib.EndDrawing();
        return;

        void DrawRectangle(float x, float y, float size, Color color) {
            var rect = new Rectangle(x, y, size, size);
            Raylib.DrawRectanglePro(rect, Vector2.One, 0f, color);
        }
    }

    private static float RandomFloat(int min, int max) {
        return Random.Next(min + 50, max - 50);
    }
}