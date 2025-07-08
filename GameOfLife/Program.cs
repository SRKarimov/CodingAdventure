using System.Numerics;
using Raylib_cs;

class Program {
    private static bool _canDrawing = true;
    private const int Cols = 320;
    private const int Rows = 180;
    private const int SquareSize = 1600 / Cols;

    private static readonly Vector2[] Grid = new Vector2[Cols * Rows];

    [STAThread]
    public static void Main() {
        Raylib.InitWindow(1600, 900, "Game of Life");
        while (!Raylib.WindowShouldClose()) {
            Input();

            if (_canDrawing)
                Update();

            Draw();
        }

        Raylib.CloseWindow();
    }

    private static void Input() {
        if (Raylib.IsKeyPressed(KeyboardKey.A)) {
            Console.WriteLine("Mouse Button Pressed");
            _canDrawing = !_canDrawing;
        }
        else if (Raylib.IsMouseButtonPressed(MouseButton.Right)) {
            _canDrawing = true;
        }
    }

    private static void Draw() {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.RayWhite);

        for (var i = 0; i < Rows * Cols; i++) {
            var rect = new Rectangle(Grid[i].X * SquareSize, Grid[i].Y * SquareSize,
                SquareSize - 1, SquareSize - 1);
            Raylib.DrawRectanglePro(rect, Vector2.One, 0f, Color.DarkBlue);
        }

        Raylib.DrawFPS(20, 870);
        Raylib.EndDrawing();
    }

    private static void Init() {
        Update();
    }

    private static void Update() {
        var rng = new Random();

        for (var i = 0; i < Cols * Rows; i++) {
            Grid[i] = new Vector2(rng.Next(0, Cols), rng.Next(0, Rows));
        }
    }
}