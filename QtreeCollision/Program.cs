using QtreeCollision;
using Raylib_cs;
using QuadTree;
using Rectangle = QuadTree.Rectangle;

class SimpleRuleLife {
    private static int _width = 1920;
    private static int _height = 1080;
    private static readonly List<Particle> Particles = [];

    [STAThread]
    public static void Main() {
        Raylib.InitWindow(_width, _height, "QTree collision");
        Raylib.SetWindowState(ConfigFlags.ResizableWindow);

        Setup();

        while (!Raylib.WindowShouldClose()) {
            Input();
            Update();
            Draw();
        }

        Raylib.CloseWindow();
    }

    private static void Draw() {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Black);

        foreach (var particle in Particles) {
            Raylib.DrawCircle(particle.X, particle.Y, particle.Radius,
                particle.HeightLight ? Color.RayWhite : Color.Gray);
        }

        Raylib.DrawFPS(20, _height - 30);
        Raylib.EndDrawing();
    }

    private static void Update() {
        var rectangle = new Rectangle(_width / 2, _height / 2, _width / 2, _height / 2);
        var quadTree = new QuadTree.QuadTree(rectangle, 4);
        foreach (var particle in Particles) {
            particle.Move();
            particle.HeightLight = false;
            quadTree.Insert(new Point(particle.X, particle.Y));
        }

        foreach (var particle in from particle in Particles
                 let points =
                     quadTree.Query(new Rectangle(particle.X, particle.Y, (int)(particle.Radius * 2),
                         (int)(particle.Radius * 2)))
                 where points.Count > 1
                 select particle) {
            particle.HeightLight = true;
        }

        quadTree = null;
    }

    private static void Input() {
        if (!Raylib.IsWindowResized()) return;

        _width = Raylib.GetScreenWidth();
        _height = Raylib.GetScreenHeight();

        Particles.Clear();
        Setup();
    }

    private static void Setup() {
        for (var i = 0; i < 10_000; ++i) {
            var particle = new Particle(Raylib.GetRandomValue(2, _width - 2), Raylib.GetRandomValue(2, _height - 2));
            Particles.Add(particle);
        }
    }
}