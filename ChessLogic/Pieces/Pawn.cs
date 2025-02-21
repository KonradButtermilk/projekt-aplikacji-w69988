using ChessLogic.Moves;

namespace ChessLogic.Pieces
{
    public class Pawn : Piece
    {

        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }
        private readonly Direction forward;

        public Pawn(Player color)
        {
            Color = color;
            if(color == Player.White)
            {
                forward = Direction.North;
            }
            else if(color == Player.Black)
            {
                forward = Direction.South;
            }
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private static bool CanMoveTo(Position position, Board board) { 
        
            return Board.IsInside(position) && board.IsEmpty(position);
        }

        private bool CanCaptureAt(Position position, Board board)
        {
           if(!Board.IsInside(position) || board.IsEmpty(position))
            {
                return false;
            }
            return board[position].Color != Color;
        }

        private IEnumerable<Move> ForwardMoves(Position from, Board board)
        {
            Position oneMovePosition = from + forward;
            if(CanMoveTo(oneMovePosition, board))
            {
               yield return new NormalMove(from, oneMovePosition);
                Position twoMovePosition = oneMovePosition + forward;

                if(!HasMoved && CanMoveTo(twoMovePosition, board))
                {
                    yield return new NormalMove(from, twoMovePosition);
                }
            }
        }   
        private IEnumerable<Move> DiagonalMoves(Position from, Board board)
        {
           foreach(Direction direction in new Direction[] {Direction.West, Direction.East})
            {
                Position to = from + forward+ direction;
                if(CanCaptureAt(to, board))
                {
                    yield return new NormalMove(from, to);
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
           return ForwardMoves(from, board).Concat(DiagonalMoves(from, board));
        }

    }
}
