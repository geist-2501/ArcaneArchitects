namespace ArcaneArchitects.Core
{
    public class GridCoord
    {
        public GridCoord(int x, int y)
        {
            Y = y;
            X = x;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"[{X},{Y}]";
        }
    }
}