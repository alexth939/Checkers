using Runtime.GameBoard;
using UnityEngine.EventSystems;

namespace Runtime.ScenePresenters
{
     internal static class PlaySceneDependencyInjector
     {
          private static PlaySceneDIContainer _container;

          internal static void Expose(PlaySceneDIContainer container) => _container = container;
          internal static void Dispose() => _container = null;

          internal static IGameBoardView FromScene(this IGameBoardView _) => _container.GameBoardView;
          internal static IPointerControl FromScene(this IPointerControl _) => _container.PointerControl;
          internal static IPointerConfiguration FromScene(this IPointerConfiguration _) => _container.PointerConfiguration;
     }
}
