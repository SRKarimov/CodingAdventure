namespace QuadTree;

public record Rectangle(int X, int Y, int Width, int Height) {
    public bool Contains(Point point) {
        return point.X >= X - Width &&
               point.X < X + Width &&
               point.Y >= Y - Height &&
               point.Y < Y + Height;
    }

    public bool Intersects(Rectangle range) {
        return !(range.X - range.Width > X + Width ||
                 range.X + range.Width < X - Width ||
                 range.Y - range.Height > Y + Height ||
                 range.Y + range.Height < Y - Height);
    }
}