
namespace SzachowaLogika
{
    public class Kierunek
    {
        //ruch xy
        public readonly static Kierunek North = new Kierunek(-1, 0);
        public readonly static Kierunek South = new Kierunek(1, 0);
        public readonly static Kierunek East = new Kierunek(0, 1);
        public readonly static Kierunek West = new Kierunek(0, -1);
        //ruch po skosie
        public readonly static Kierunek NorthEast = North + East;
        public readonly static Kierunek NorthWest = North + West;
        public readonly static Kierunek SouthEast = South + East;
        public readonly static Kierunek SouthWest = South + West;

        public int RowDelta { get; }
        public int ColumnDelta { get; }
        public Kierunek(int rowDelta, int columnDelta)
        {
            RowDelta = rowDelta;
            ColumnDelta = columnDelta;
        }
        public static Kierunek operator +(Kierunek dir1, Kierunek dir2)
        {
            return new Kierunek(dir1.RowDelta + dir2.RowDelta, dir1.ColumnDelta + dir2.ColumnDelta);
        }
        public static Kierunek operator * (int scalar, Kierunek dir)
        {
            return new Kierunek(scalar * dir.RowDelta, scalar * dir.ColumnDelta);
        }
    }
}

