namespace SzachowaLogika
{
    //użyłem enum bo jest ograniczony zestaw wartości i będę mógł nadawać im określone nazwy 
    public enum Player 
    { 
        None,
        White,
        Black
    }
    public static class PlayerExtentions
    {
        //biorę gracza jako parametr i zwraca przeciwnika 
        public static Player Opponent(this Player player)
        {
            return player switch
            {
                Player.White => Player.Black,
                Player.Black => Player.White,
                _ => Player.None,
            };
        }
    }
}
