using System;
using System.Collections.Generic;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
    internal sealed class GameFlowModel
    {
        private readonly Dictionary<Type, float> _stateTimeoutValues = new()
        {
            [typeof(PingPlayerState)] = 10.0f,
            [typeof(ForcingRandomMoveState)] = 0,
            [typeof(PlayerMovingState)] = 30.0f
        };

        private Dictionary<BoardSide, IGameBoardPlayer> _localPlayers;
        private GameCommandsChannel _commandsChannel;
        private GameMovesChannel _movesChannel;

        internal GameFlowModel(Action<GameFlowOptions> options = default)
        {
            var optionsSetter = new GameFlowOptions(options);
            optionsSetter.Extract(this);

            _localPlayers ??= new Dictionary<BoardSide, IGameBoardPlayer>();
            _commandsChannel ??= new GameCommandsChannel();
            _movesChannel ??= new GameMovesChannel();
        }

        internal IReadOnlyDictionary<Type, float> StateTimeoutValues => _stateTimeoutValues;
        internal IReadOnlyDictionary<BoardSide, IGameBoardPlayer> LocalPlayers => _localPlayers;
        internal GameCommandsChannel CommandsChannel => _commandsChannel;
        internal GameMovesChannel MovesChannel => _movesChannel;

        internal sealed class GameFlowOptions
        {
            public GameFlowOptions(Action<GameFlowOptions> args)
            {
                args.Invoke(this);
            }

            internal Dictionary<BoardSide, IGameBoardPlayer> LocalPlayers { private get; set; }
            internal GameCommandsChannel CommandsChannel { private get; set; } = null;
            internal GameMovesChannel MovesChannel { private get; set; } = null;

            internal void Extract(GameFlowModel target)
            {
                target._localPlayers = LocalPlayers;
                target._commandsChannel = CommandsChannel;
                target._movesChannel = MovesChannel;
            }
        }
    }
}
