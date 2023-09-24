using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpinnerSpawner : MonoBehaviour
{
    private const float DISTANCEFROMCENTER = 100;
    [SerializeField] private SpinnerContent contentPrefab;

    [FormerlySerializedAs("contentHolder")] [SerializeField]
    private SpinnerContentHolderSO contentHolderSo;

    public void Init()
    {
        CreateChildren(0);
    }

    private void CreateChildren(int tier)
    {
        var direction = Vector3.up;

        for (int i = 0; i < SpinnerUtilities.HOLECOUNT; i++)
        {
            var content = Instantiate(contentPrefab, transform);
            var contentOffset = Quaternion.AngleAxis(SpinnerUtilities.PERCOUNTANGLE * i, Vector3.forward) * direction *
                                DISTANCEFROMCENTER;
            content.transform.position =
                transform.position + contentOffset;

            var contentDir = contentOffset.normalized;

            //Todo To avoid quaternion axis up calculation
            if (Vector3.Dot(Vector3.down, contentDir) > 0.99f)
            {
                content.transform.eulerAngles = Vector3.forward * 180;
            }
            else
            {
                content.transform.rotation = Quaternion.FromToRotation(Vector3.up, contentDir);
            }


            var targetContentData = GetTargetContentData(i);

            contentPrefab.Init(targetContentData.contentId, targetContentData.contentSprite,
                targetContentData.tierGainList[tier].ToString());
        }
    }

    private SpinnerContentItemSO GetTargetContentData(int i)
    {
        return contentHolderSo.itemContents[i];
    }
}