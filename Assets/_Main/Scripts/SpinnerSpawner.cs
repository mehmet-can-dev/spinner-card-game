using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerSpawner : MonoBehaviour
{
    private const float DISTANCEFROMCENTER = 100;
    [SerializeField] private SpinnerContent contentPrefab;

    [SerializeField] private List<Sprite> _sprites;

    public void Init()
    {
        CreateChilds();
    }

    private void CreateChilds()
    {
        var direction = Vector3.up * DISTANCEFROMCENTER;

        for (int i = 0; i < Spinner.HOLECOUNT; i++)
        {
            var content = Instantiate(contentPrefab, transform);
            var contentDir = Quaternion.AngleAxis(Spinner.PERCOUNTANGLE * i, Vector3.forward) * direction;
            content.transform.position =
                transform.position + contentDir;
            content.transform.up = contentDir;
            contentPrefab.Init(_sprites[i], "00000");
        }
    }
}