using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChessLogic;

namespace ChessUI
{
    // Statyczna klasa obsługująca obrazy figur szachowych
    public static class Images
    {
        // Słownik przechowujący źródła obrazów białych figur
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            {PieceType.Pawn, LoadImage("Assets/wP.png")},    
            {PieceType.Rook, LoadImage("Assets/wR.png")},     
            {PieceType.Knight, LoadImage("Assets/wN.png")},
            {PieceType.Bishop, LoadImage("Assets/wB.png")},
            {PieceType.Queen, LoadImage("Assets/wQ.png")},    
            {PieceType.King, LoadImage("Assets/wK.png")},     // Obraz króla
        };

        // Słownik przechowujący źródła obrazów czarnych figur
        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            {PieceType.Pawn, LoadImage("Assets/BP.png")},     // Obraz pionka
            {PieceType.Rook, LoadImage("Assets/bR.png")},     // Obraz wieży
            {PieceType.Knight, LoadImage("Assets/bN.png")},   // Obraz skoczka
            {PieceType.Bishop, LoadImage("Assets/Bb.png")},   // Obraz gońca
            {PieceType.Queen, LoadImage("Assets/BQ.png")},    // Obraz królowej
            {PieceType.King, LoadImage("Assets/bK.png")},     // Obraz króla
        };

        // Metoda ładująca obraz z podanej ścieżki pliku
        private static ImageSource LoadImage(string filePath) =>
            new BitmapImage(new Uri(filePath, UriKind.Relative));

        // Metoda zwracająca odpowiedni obraz dla danej figury
        // Zależnie od koloru figury zwracany jest obraz z odpowiedniego słownika
        public static ImageSource GetImage(Piece piece)
        {
            if (piece == null)
            {
                return null;  // Jeśli figura jest pusta (null), zwróć null
            }

            // Wybierz odpowiedni słownik źródeł obrazów na podstawie koloru figury
            var sources = piece.Color == Player.White ? whiteSources : blackSources;
            return sources[piece.Type];  // Zwróć obraz odpowiadający typowi figury
        }
    }
}
