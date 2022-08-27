using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runtime.WindowViews;

namespace Runtime.ScenePresenters
{
     internal class LobbyScenePresenter: MonoBehaviour
     {
          [SerializeField] private AuthorizationSceneContainer _dependencies;

          [Serializable]
          private sealed class AuthorizationSceneContainer
          {
               //internal ITransitionsView TransitionsView => _transitionsView;
               internal ILobbyWindowView LobbyWindow => _lobbyWindow;
               internal IOnlinePrepareWindow OnlinePrepareWindow => _onlinePrepareWindow;
               internal IOfflinePrepareWindow OfflinePrepareWindow => _offlinePrepareWindow;

               [SerializeField] private LobbyWindowView _lobbyWindow;
               [SerializeField] private OnlinePrepareWindow _onlinePrepareWindow;
               [SerializeField] private OfflinePrepareWindow _offlinePrepareWindow;
               //[SerializeField] private TransitionsView _transitionsView;
          }
     }
}
