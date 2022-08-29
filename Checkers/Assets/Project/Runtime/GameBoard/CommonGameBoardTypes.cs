namespace Runtime.GameBoard
{
     internal enum BoardViewMode
     {
          OrthographicTopDown,
          PerspectiveFromSide
     }

     internal enum CheckerType
     {
          RegularChecker,
          QueenChecker
     }

     internal enum CheckersGameType
     {
          Checkers36,
          Checkers64,
          Checkers100,
          GiveAway64
     }

     internal struct BoardSide
     {
          public int? index;

          internal static BoardSide UpperSide => new() { index = 0 };
          internal static BoardSide LowerSide => new() { index = 1 };
          internal static BoardSide BothSides => new() { index = 2 };

          public static implicit operator int(BoardSide playerPlacement) => playerPlacement.index.Value;
     }
}
