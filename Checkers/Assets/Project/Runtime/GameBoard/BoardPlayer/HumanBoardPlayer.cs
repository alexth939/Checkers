using System;
using UnityEngine.EventSystems;
using External.DelegateExtensions;

namespace Runtime.GameBoard
{
    internal sealed class HumanBoardPlayer: IGameBoardPlayer
    {
        private IHumanMindInterpreter _mindInterpreter;

        internal HumanBoardPlayer(IGameBoardPresenter gameBoard)
        {
            //argsSetter.Extract(out var results);

            _mindInterpreter = new HumanMindInterpreter(args =>
            {

            });
        }

        private Action<byte[]> MoveChecker;

        public void BeginTurn(Action<byte[]> value)
        {
            throw new NotImplementedException();
        }

        public void CancelTurn()
        {
            throw new NotImplementedException();
        }

        public void IsReady(Action positiveResponse)
        {
            positiveResponse.Invoke();
        }

        internal void MakeMove(Action<byte[]> move)
        {

        }

        internal class PlayerDataPort
        {

        }
    }
}
