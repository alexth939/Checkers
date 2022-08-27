namespace Runtime.GameFlow
{
     internal interface ITurnStatesCollection
     {
          TurnState First { get; }
          bool TryGetNext(TurnState currentNode, out TurnState nextNode);
     }
}
