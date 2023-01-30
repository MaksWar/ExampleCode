using System;
using System.Collections;
using UnityEngine;

namespace Additions.Extensions
{
    public static class CoroutineExtensions
    {
        public static Coroutine RepeatingCall(this MonoBehaviour @this, float delay, float interval, Action call)
        {
            return @this.StartCoroutine(RepeatingCallRoutine());

            IEnumerator RepeatingCallRoutine()
            {
                yield return new WaitForSeconds(delay);

                while (true)
                {
                    yield return new WaitForSeconds(interval);
                    call();
                }
            }
        }

        public static Coroutine DelayedCall(this MonoBehaviour @this, float delay, Action call)
        {
            return @this.StartCoroutine(DelayedCallRoutine());

            IEnumerator DelayedCallRoutine()
            {
                yield return new WaitForSeconds(delay);
                call();
            }
        }

        public static Coroutine LerpFixedUpdate(this MonoBehaviour @this, float duration, Action<float> onUpdate,
            Func<float, float> ease)
        {
            return @this.StartCoroutine(Lerp());

            IEnumerator Lerp()
            {
                float t = 0;
                while (t < 1)
                {
                    onUpdate?.Invoke(ease.Invoke(t));
                    t += Time.fixedDeltaTime / duration;
                    yield return new WaitForFixedUpdate();
                }

                onUpdate?.Invoke(ease.Invoke(1));
            }
        }

        public static Coroutine LerpUpdate(this MonoBehaviour @this, float duration, Action<float> onUpdate,
            Func<float, float> ease)
        {
            return @this.StartCoroutine(Lerp());

            IEnumerator Lerp()
            {
                float t = 0;
                while (t < 1)
                {
                    onUpdate?.Invoke(ease.Invoke(t));
                    t += Time.fixedDeltaTime / duration;
                    yield return new WaitForEndOfFrame();
                }

                onUpdate?.Invoke(ease.Invoke(1));
            }
        }
    }
}