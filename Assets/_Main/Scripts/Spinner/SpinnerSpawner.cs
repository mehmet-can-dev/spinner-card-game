using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpinnerSpawner : MonoBehaviour
{
    private const float DISTANCEFROMCENTER = 150;

    [Header("Scene References")] [SerializeField]
    private Image uiSpinnerImage;

    [Header("Project References")] [SerializeField]
    private SpinnerContent contentPrefab;

    [SerializeField] private SpinnerSettingsSO spinnerSettingsSo;

    private int currentTier = 0;

    private List<SpinnerContent> createdSpinnerContentUis;

    public void Init()
    {
        createdSpinnerContentUis = InstantiateContents();
    }

    public void CreateTier(int tier)
    {
        var _tier = tier > spinnerSettingsSo.spinnerTypes.Count ? tier % spinnerSettingsSo.spinnerTypes.Count : tier;

        var spinnerTypeSo = spinnerSettingsSo.spinnerTypes[_tier];

        CreateSpinner(spinnerTypeSo.spinnerSprite);

        var contents = SelectContents(spinnerTypeSo);

        contents.Shuffle();

        FillContentUIs(createdSpinnerContentUis, contents);
    }

    private List<SpinnerContent> InstantiateContents()
    {
        var spinnerContents = new List<SpinnerContent>();
        var direction = Vector3.up;
        for (int i = 0; i < SpinnerUtilities.HOLECOUNT; i++)
        {
            var content = Instantiate(contentPrefab, transform);
            var contentOffset = Quaternion.AngleAxis(SpinnerUtilities.PERCOUNTANGLE * i, Vector3.forward) * direction *
                                DISTANCEFROMCENTER;
            content.transform.localPosition = contentOffset;

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

            spinnerContents.Add(content);
        }

        return spinnerContents;
    }

    private List<SpinnerContentSO> SelectContents(SpinnerTypeSO typeSo)
    {
        var contents = new List<SpinnerContentSO>();

        for (int i = 0; i < typeSo.definitelyContents.Count; i++)
        {
            contents.Add(typeSo.definitelyContents[i]);
        }

        for (int i = 0; i < typeSo.bombContents.Count; i++)
        {
            contents.Add(typeSo.bombContents[i]);
        }

        var tempList = new List<SpinnerContentItemSO>(typeSo.possibilityContents);
        var bag = new MarbleBag<SpinnerContentItemSO>(tempList);

        var remainingCount = SpinnerUtilities.HOLECOUNT - contents.Count;

        for (int i = 0; i < remainingCount; i++)
        {
            var c = bag.PickRandom();
            contents.Add(c);
        }

        Debug.Log(SpinnerUtilities.LogContentList(contents));

        return contents;
    }

    private void FillContentUIs(List<SpinnerContent> contentUis, List<SpinnerContentSO> contentSos)
    {
        if (contentUis.Count != contentSos.Count)
            Debug.LogError("ContentUi count and contentSo count not match !");

        for (int i = 0; i < contentUis.Count; i++)
        {
            
            if (contentSos[i] is SpinnerContentItemSO)
            {
                SpinnerContentItemSO itemContent = (SpinnerContentItemSO)contentSos[i];
                contentUis[i].Init(contentSos[i].contentId, itemContent.contentSprite,
                    itemContent.tierGainList[0].ToString());
            }
            else if (contentSos[i] is SpinnerContentBombSO)
            {
                SpinnerContentBombSO bombContent = (SpinnerContentBombSO)contentSos[i];
                contentUis[i].Init(contentSos[i].contentId, bombContent.contentSprite, "");
            }
            
        }
    }

    private void CreateSpinner(Sprite sprite)
    {
        uiSpinnerImage.sprite = sprite;
    }
    
}