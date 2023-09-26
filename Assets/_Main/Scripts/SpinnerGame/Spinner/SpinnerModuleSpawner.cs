using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpinnerModuleSpawner : MonoBehaviour
{
    private float DISTANCEFROMCENTER = 150;

    [Header("Child References")] [SerializeField]
    private Image uiSpinnerImage;

    [SerializeField] private Image uiSpinnerIndicator;
    [SerializeField] private Transform contentParent;

    [Header("Project References")] [SerializeField]
    private SpinnerContentUi contentUiPrefab;


    private int currentTier = 0;

    private List<SpinnerContentUi> createdSpinnerContentUis;
    private List<ItemData> createdItemData;
    

    public void Init()
    {
      
        createdSpinnerContentUis = InstantiateContents();
    }

    public void CreateTier(int tier, SpinnerSettingsSO spinnerSettingsSo)
    {
        var _tier = ListUtilities.GetModdedIndex(spinnerSettingsSo.spinnerTypes, tier);
        var spinnerTypeSo = spinnerSettingsSo.spinnerTypes[_tier];

        CreateSpinner(spinnerTypeSo.spinnerSprite, spinnerTypeSo.indicatorSprite);

        var contents = SelectContents(spinnerTypeSo);

        contents.Shuffle();

        SpinnerUtilities.LogContentList(contents);

        createdItemData = CalculateRewards(createdSpinnerContentUis, contents, _tier);

        FillContentUIs(createdSpinnerContentUis, createdItemData);
    }

   

    public ItemData GetRewardDataFromIndex(int index)
    {
        if (index >= createdItemData.Count)
            Debug.LogError("Index must be lower created reward count");

        return createdItemData[index];
    }

    public SpinnerContentUi GetContentUiFromIndex(int index)
    {
        if (index >= createdSpinnerContentUis.Count)
            Debug.LogError("Index must be lower created content ui count");

        return createdSpinnerContentUis[index];
    }

    private List<ItemData> CalculateRewards(List<SpinnerContentUi> contentUis, List<SpinnerContentSO> contentSos,
        int tier)
    {
        if (contentUis.Count != contentSos.Count)
            Debug.LogError("ContentUi count and contentSo count not match !");

        var itemList = new List<ItemData>();

        for (int i = 0; i < contentUis.Count; i++)
        {
            if (contentSos[i] is SpinnerContentItemSO itemContent)
            {
                var gainAmount = SpinnerUtilities.CalculateItemGainAmount(itemContent, tier);

                var rwData = new RewardItemData()
                {
                    itemId = contentSos[i].contentId,
                    itemSprite = contentSos[i].contentSprite,
                    rewardAmount = gainAmount
                };

                rwData.rewardAmount = gainAmount;
                itemList.Add(rwData);
            }
            else if (contentSos[i] is SpinnerContentBombSO)
            {
                var bombData = new BombItemData()
                {
                    itemId = contentSos[i].contentId,
                    itemSprite = contentSos[i].contentSprite,
                };

                itemList.Add(bombData);
            }
        }

        return itemList;
    }

    private List<SpinnerContentUi> InstantiateContents()
    {
        var spinnerContents = new List<SpinnerContentUi>();
        var direction = Vector3.up;
        for (int i = 0; i < SpinnerLogic.HOLECOUNT; i++)
        {
            var content = Instantiate(contentUiPrefab, contentParent);
            var contentOffset = Quaternion.AngleAxis(SpinnerLogic.PERCOUNTANGLE * i, Vector3.forward * -1) *
                                direction *
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

        var remainingCount = SpinnerLogic.HOLECOUNT - contents.Count;

        for (int i = 0; i < remainingCount; i++)
        {
            var c = bag.PickRandom();
            contents.Add(c);
        }

        // Debug.Log(SpinnerUtilities.LogContentList(contents));

        return contents;
    }

    private void FillContentUIs(List<SpinnerContentUi> contentUis, List<ItemData> itemList)
    {
        if (contentUis.Count != itemList.Count)
            Debug.LogError("ContentUi count and itemList count not match !");

        for (int i = 0; i < contentUis.Count; i++)
        {
            if (itemList[i] is RewardItemData)
            {
                var rwItem = (RewardItemData)itemList[i];
                contentUis[i].Init(rwItem.itemId, rwItem.itemSprite, rwItem.rewardAmount);
            }
            else if (itemList[i] is BombItemData)
            {
                var rwItem = (BombItemData)itemList[i];
                contentUis[i].Init(rwItem.itemId, rwItem.itemSprite);
            }

            contentUis[i].StartOpeningAnimation();
        }
    }

    private void CreateSpinner(Sprite spinnerSprite, Sprite indicatorSprite)
    {
        uiSpinnerImage.sprite = spinnerSprite;
        uiSpinnerIndicator.sprite = indicatorSprite;
        contentParent.rotation = Quaternion.identity;
    }
}