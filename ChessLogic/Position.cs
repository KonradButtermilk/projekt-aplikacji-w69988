namespace ChessLogic
{
    // Struktura reprezentująca pozycję na szachownicy
    public readonly struct Position
    {
        // Właściwości przechowujące wiersz i kolumnę pozycji
        public int Row { get; }  // Przechowuje wiersz pozycji na szachownicy (0-7)
        public int Column { get; }  // Przechowuje kolumnę pozycji na szachownicy (0-7)

        // Konstruktor inicjalizujący pozycję z podanym wierszem i kolumną
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        // Przeciążony operator dodawania, pozwalający na dodanie kierunku do pozycji
        // Przesuwa pozycję o zadany kierunek (DeltaRow, DeltaColumn)
        public static Position operator +(Position pos, Direction dir)
        {
            return new Position(pos.Row + dir.DeltaRow, pos.Column + dir.DeltaColumn);
        }

        // Metoda próbująca sparsować pozycję z ciągu znaków w formacie "a1", "h8" itp.
        // Sprawdza poprawność formatu i zamienia znaki na odpowiednie indeksy wiersza i kolumny
        public static bool TryParse(string s, out Position position)
        {
            position = new Position(0, 0);  // Inicjalizujemy pozycję jako (0, 0)
            if (s.Length != 2) return false;  // Sprawdzamy, czy długość ciągu wynosi 2 znaki

            int col = s[0] - 'a';  // Konwertujemy pierwszy znak (kolumna) na indeks od 0 do 7
            int row = 8 - (s[1] - '0');  // Konwertujemy drugi znak (wiersz) na indeks od 0 do 7

            // Sprawdzamy, czy wartości wiersza i kolumny mieszczą się w granicach szachownicy (0-7)
            if (row < 0 || row >= 8 || col < 0 || col >= 8)
                return false;

            position = new Position(row, col);  // Ustawiamy przetworzoną pozycję
            return true;
        }

        // Metoda zwracająca reprezentację pozycji jako ciąg znaków w formacie "a1", "h8" itp.
        // Zamienia indeksy kolumny i wiersza na odpowiednie znaki
        public override string ToString() => $"{(char)('a' + Column)}{8 - Row}";

        // Przeciążony operator porównania pozycji, sprawdzający równość wiersza i kolumny
        public static bool operator ==(Position a, Position b)
        {
            return a.Row == b.Row && a.Column == b.Column;
        }

        // Przeciążony operator porównania pozycji, sprawdzający nierówność wiersza i kolumny
        public static bool operator !=(Position a, Position b)
        {
            return !(a == b);
        }

        // Metoda sprawdzająca równość pozycji
        public override bool Equals(object obj)
        {
            if (obj is Position pos)
            {
                return this == pos;
            }
            return false;
        }

        // Metoda zwracająca hash code pozycji, używany w strukturach danych opartych na haszowaniu
        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);  // Kombinacja hash code wiersza i kolumny
        }
    }
}
