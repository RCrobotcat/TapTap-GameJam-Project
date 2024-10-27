using UnityEngine;
using UnityEngine.UI;

public class QuestNameBtn : MonoBehaviour
{
    public Text questNameTxt;
    public QuestData_SO currentQuestData;
    public Text questContentTxt;

    void Awake()
    {
        if (GetComponent<Button>() != null)
            GetComponent<Button>().onClick.AddListener(UpdateQuestContent);
    }

    void UpdateQuestContent()
    {
        questContentTxt.text = currentQuestData.description;
        QuestUI.Instance.SetUpRequirements(currentQuestData);

        foreach (Transform item in QuestUI.Instance.rewardsTransform)
            Destroy(item.gameObject);

        foreach (var item in currentQuestData.questRewards)
            QuestUI.Instance.SetUpRewardItem(item.itemData, item.amount);
    }

    public void SetUpNameBtn(QuestData_SO questData)
    {
        currentQuestData = questData;

        if (questData.isFinished)
            questNameTxt.text = questData.questName + " (ÒÑÍê³É)";
        else questNameTxt.text = questData.questName;
    }
}