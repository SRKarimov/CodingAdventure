using Raylib_cs;

namespace SimpleRuleLife;

class Particle(float x, float y, float vx, float vy, Color color) {
    public float X { get; set; } = x;
    public float Y { get; set; } = y;
    public float VX { get; set; } = vx;
    public float VY { get; set; } = vy;
    public float Size { get; set; } = 2;
    public Color Color { get; set; } = color;
}