namespace QtreeCollision;

public class Particle(int x, int y) {
    private static Random random = new Random();
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public float Radius { get; set; } = 2f;
    public bool HeightLight { get; set; } =  false;

    public bool Intersects(Particle other) {
        var dist = Distance(X, Y, other.X, other.Y);
        return (dist <= Radius + Radius);
    }

    public void Move() {
        X += random.Next(-1, 2);
        Y += random.Next(-1, 2);
    }

    private double Distance(int x1, int y1, int x2, int y2) {
        return Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
    }
}
