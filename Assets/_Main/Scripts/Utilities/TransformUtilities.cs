using System;
using System.Collections;
using UnityEngine;

public static class TransformUtilities
{
    public static IEnumerator MoveTransform(Transform transform, Transform targetTransform, float duration,
        AnimationCurve curve, Action onComplete)
    {
        float elapsedTime = 0;
        Vector3 startPos = transform.position;

        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;

            transform.position =
                Vector3.Lerp(startPos, targetTransform.position, curve.Evaluate(elapsedTime / duration));
            yield return null;
        }

        transform.position = targetTransform.position;

        onComplete?.Invoke();
    }
}