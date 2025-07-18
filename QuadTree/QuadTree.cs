namespace QuadTree;

public class QuadTree(Rectangle boundary, int capacity) {
    private Rectangle Boundary { get; } = boundary;
    private int Capacity { get; } = capacity;

    private List<Point> Points { get; } = [];
    
    private QuadTree? NorthEast { get; set; }
    private QuadTree? NorthWest { get; set; }
    private QuadTree? SouthEast { get; set; }
    private QuadTree? SouthWest { get; set; }
    
    private bool Divided { get; set; }

    public List<Point> Query(Rectangle range) {
        var found = new List<Point>();
        if(!range.Intersects(boundary)) {
            return found;
        }

        foreach (var point in Points) {
            if (range.Contains(point)) {
                found.Add(point);
            }
        }

        if (Divided) {
            if (NorthWest != null) found.AddRange(NorthWest.Query(range));
            if (NorthEast != null) found.AddRange(NorthEast.Query(range));
            if (SouthWest != null) found.AddRange(SouthWest.Query(range));
            if (SouthEast != null) found.AddRange(SouthEast.Query(range));
        }

        return found;
    }
    
    public bool Insert(Point point) {
        if (!Boundary.Contains(point)) return false;
        
        if (Points.Count < Capacity) {
            Points.Add(point);
            return true;
        }

        if (!Divided) {
            Subdivide();
            Divided = true;
        }

        if (NorthEast != null && NorthEast.Insert(point)) {
            return true;
        }

        if (NorthWest != null && NorthWest.Insert(point)) {
            return true;
        }

        if (SouthEast != null && SouthEast.Insert(point)) {
            return true;
        }

        return SouthWest != null && SouthWest.Insert(point);
    }

    private void Subdivide() {
        var ne  = new Rectangle(Boundary.X + Boundary.Width / 2, Boundary.Y - Boundary.Height / 2, Boundary.Width / 2, Boundary.Height / 2);
        NorthEast = new QuadTree(ne, Capacity);
        
        var nw = new Rectangle(Boundary.X - Boundary.Width / 2, Boundary.Y - Boundary.Height / 2, Boundary.Width / 2, Boundary.Height / 2);
        NorthWest = new QuadTree(nw, Capacity);
        
        var se = new Rectangle(Boundary.X + Boundary.Width / 2, Boundary.Y + Boundary.Height / 2, Boundary.Width / 2, Boundary.Height / 2);
        SouthEast = new QuadTree(se, Capacity);
        
        var sw = new Rectangle(Boundary.X - Boundary.Width / 2, Boundary.Y + Boundary.Height / 2, Boundary.Width / 2, Boundary.Height / 2);
        SouthWest = new QuadTree(sw, Capacity);
    }
}