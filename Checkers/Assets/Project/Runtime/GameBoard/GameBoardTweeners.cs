using System;
using System.Collections;
using UnityEngine;

namespace Runtime.GameBoard
{
    internal sealed class GameBoardTweeners
    {
        internal static IEnumerator MoveChecker(Action<MoveCheckerActionArgs> argsSetter)
        {
            var args = new MoveCheckerActionArgs();
            argsSetter.Invoke(args);

            var startPosition = args.Checker.transform.position;

            var scaleKeys = new Keyframe[]
            {
                    new Keyframe(time: 0.0f, value: 1.0f, 1, 1),
                    new Keyframe(time: 0.5f, value: 1.5f, 0, 0),
                    new Keyframe(time: 1.0f, value: 1.0f, -1, -1)
            };
            var animationCurve = new AnimationCurve(scaleKeys);

            for(float i = 0; i < args.Duration; i += Time.deltaTime)
            {
                float t = i / args.Duration;
                args.Checker.transform.position = Vector3.Lerp(startPosition, args.Destination, t);
                args.Checker.transform.localScale = Vector3.one * animationCurve.Evaluate(t);
                yield return new WaitForEndOfFrame();
            }

            args.Checker.transform.position = args.Destination;

            args.OnDone?.Invoke();
        }

        internal class MoveCheckerActionArgs
        {
            internal CheckerView Checker;
            internal Vector3 Destination;
            internal float Duration;
            internal Action OnDone;
        }
    }
}
