namespace Journeys
{
    public struct Journey
    {
        public (int X, int Y, char Direction) Initial;
        public string Commands;
        public (int X, int Y, char Direction) Final;
        public override string ToString()
        {
            return $"{Initial.X} {Initial.Y} {Initial.Direction}\n" + Commands + "\n" + $"{Final.X} {Final.Y} {Final.Direction}";
        }
    }

}
