using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpinnerEditorBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject contentPrefab;
    [SerializeField] private float distanceFromCenter;

    [ContextMenu("SeparateChild")]
    private void SeparateChild()
    {
        var direction = Vector3.up * distanceFromCenter;

        for (int i = 0; i < Spinner.HOLECOUNT; i++)
        {
            var content = PrefabUtility.InstantiatePrefab(contentPrefab, transform) as GameObject;
            var contentDir = Quaternion.AngleAxis(Spinner.PERCOUNTANGLE * i, Vector3.forward) * direction;
            content.transform.position =
                transform.position + contentDir;
            content.transform.up = contentDir ;
        }
    }
}