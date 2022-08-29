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
          internal static IPointerControl FromScene(this IPointerControl _, BoardViewMode orientation)
          {
               return orientation switch
               {
                    BoardViewMode.OrthographicTopDown => _container.PointerControl,
                    BoardViewMode.PerspectiveFromSide => throw new System.NotImplementedException(),
                    _ => null
               };
          }
          internal static IPointerConfiguration FromScene(this IPointerConfiguration _) => _container.PointerConfiguration;
     }
}
