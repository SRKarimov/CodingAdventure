using Raylib_cs;

class Program {
    [STAThread]
    public static void Main() {
        Raylib.InitWindow(1600, 900, "Game of Life");
        while (!Raylib.WindowShouldClose()) {
            // Input();
            // Update();
            Draw();
        }

        Raylib.CloseWindow();
    }

    private static void Draw() {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.RayWhite);
        Raylib.DrawText("Congrats! You created your first window!", 190, 200, 20, Color.LightGray);
        Raylib.DrawFPS(20, 870);
        Raylib.EndDrawing();
    }
}