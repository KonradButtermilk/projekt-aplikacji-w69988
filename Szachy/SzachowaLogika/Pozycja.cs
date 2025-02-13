namespace SzachowaLogika
{
    public class Pozycja
    {
        public int Row { get; }
        public int Column { get; }

        public Pozycja(int row, int column) 
        {
            Row = row;
            Column = column;
        }
        //Sprawdzam jakiego koloru jest pole - parzyste: białe, nieparzyste: czarne
        public Player SquareColor()
        {
            if ((Row + Column) % 2 == 0)
            {
                return Player.White;
            }
            return Player.Black;
        }
        //
        public override bool Equals(object obj)
        {
            return obj is Pozycja pozycja &&
                   Row == pozycja.Row &&
                   Column == pozycja.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
        //dzięki temu mogę porównywać pozycje za pomocą ==
        public static bool operator ==(Pozycja left, Pozycja right)
        {
            return EqualityComparer<Pozycja>.Default.Equals(left, right);
        }

        public static bool operator !=(Pozycja left, Pozycja right)
        {
            return !(left == right);
        }
        //Biorę pozycję i kierunek jako parametry i zwraca je jako jeden krok w wybranym kierunku
        public static Pozycja operator +(Pozycja pos, Kierunek dir)
        {
            return new Pozycja(pos.Row + dir.RowDelta, pos.Column + dir.ColumnDelta);
        }
        /* Przykład
         * Pozycja from = new Pozycja(0, 6);
         * Pozycja to = from + 3 * Kierunek.SouthWest; 
        */
    }
}
