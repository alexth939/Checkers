using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runtime.WindowViews;

namespace Runtime.ScenePresenters
{
     public class LobbyScenePresenter: MonoBehaviour
     {
          [SerializeField] private AuthorizationSceneContainer _dependencies;

          [Serializable]
          private sealed class AuthorizationSceneContainer
          {
               //public ITransitionsView TransitionsView => _transitionsView;
               public ILobbyWindowView LobbyWindow => _lobbyWindow;
               public IOnlinePrepareWindow OnlinePrepareWindow => _onlinePrepareWindow;
               public IOfflinePrepareWindow OfflinePrepareWindow => _offlinePrepareWindow;

               [SerializeField] private LobbyWindowView _lobbyWindow;
               [SerializeField] private OnlinePrepareWindow _onlinePrepareWindow;
               [SerializeField] private OfflinePrepareWindow _offlinePrepareWindow;
               //[SerializeField] private TransitionsView _transitionsView;
          }
     }
}
