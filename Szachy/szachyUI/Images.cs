using System;
using System.Windows.Media;
using System.Collections.Generic;
using SzachowaLogika;
using System.Windows.Media.Imaging;



namespace szachyUI
{
    public static class Images
    {
        private static readonly Dictionary<RodzajBierek, ImageSource> whiteSources = new()
        {
            { RodzajBierek.Pawn, LoadImage("Zasoby/PawnW.png") },
            { RodzajBierek.Knight, LoadImage("Zasoby/KnightW.png") },
            { RodzajBierek.Bishop, LoadImage("Zasoby/BishopW.png") },
            { RodzajBierek.Rook, LoadImage("Zasoby/RookW.png") },
            { RodzajBierek.Queen, LoadImage("Zasoby/QueenW.png") },
            { RodzajBierek.King, LoadImage("Zasoby/KingW.png") }
        };
        
        
        private static readonly Dictionary<RodzajBierek, ImageSource> blackSources = new()
        {
            { RodzajBierek.Pawn, LoadImage("Zasoby/PawnB.png") },
            { RodzajBierek.Knight, LoadImage("Zasoby/KnightB.png") },
            { RodzajBierek.Bishop, LoadImage("Zasoby/BishopB.png") },
            { RodzajBierek.Rook, LoadImage("Zasoby/RookB.png") },
            { RodzajBierek.Queen, LoadImage("Zasoby/QueenB.png") },
            { RodzajBierek.King, LoadImage("Zasoby/KingB.png") }
        };

        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }
        public static ImageSource GetImage(Player color, RodzajBierek type)
        {
            return color switch
            {
                Player.White => whiteSources[type],
                Player.Black => blackSources[type],
                _ => null
            };
        }

        public static ImageSource GetImage(Piece bierka)
        { 
            if (bierka == null)
            {
            return null;
            }
        

            return GetImage(bierka.Color, bierka.Type);
        }
    }
}
