namespace Runtime.GameBoard
{
     internal struct CheckerModel
     {
          /// <summary>
          /// Raw (on board) position.
          /// </summary>
          internal byte Position;
          internal CheckerType Type;

          public static implicit operator CheckerModel(byte rawPosition) => new()
          {
               Position = rawPosition,
               Type = CheckerType.RegularChecker
          };

          public static implicit operator CheckerModel((byte rawPosition, CheckerType type) checkerData) => new()
          {
               Position = checkerData.rawPosition,
               Type = checkerData.type
          };
     }
}
