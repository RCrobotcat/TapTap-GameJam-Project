using UnityEngine;
using UnityEngine.UI;

public class QuestUI : Singleton<QuestUI>
{
    [Header("Elements")]
    public GameObject questPanel;
    public GameObject SideQuestPanel;
    public GameObject itemTooltip;
    public GameObject QuestCompletedText;
    bool isOpen;

    [Header("Quest Name")]
    public RectTransform questListTransform;
    public RectTransform sideQuestListTransform;
    public QuestNameBtn questNameBtn;
    public QuestNameBtn sideQuestNameBtn;

    [Header("Quest Content")]
    public Text questContentTxt;

    [Header("Requirements")]
    public RectTransform requirementsTransform;
    public QuestRequirement questRequirement;

    [Header("Rewards")]
    public RectTransform rewardsTransform;
    public ItemUI rewardUI;

    void Start()
    {
        SetUpSideQuestList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isOpen = !isOpen;
            questPanel.SetActive(isOpen);
            questContentTxt.text = string.Empty;

            if (!isOpen)
                itemTooltip.gameObject.SetActive(false);

            SetUpQuestList();
        }
    }

    public void SetUpQuestList()
    {
        foreach (Transform item in questListTransform)
            Destroy(item.gameObject);

        foreach (Transform item in requirementsTransform)
            Destroy(item.gameObject);

        foreach (Transform item in rewardsTransform)
            Destroy(item.gameObject);

        foreach (var task in QuestManager.Instance.questTasks)
        {
            var newTask = Instantiate(questNameBtn, questListTransform);
            newTask.SetUpNameBtn(task.questData);
            newTask.questContentTxt = questContentTxt;
        }
    }

    public void SetUpSideQuestList()
    {
        foreach (Transform item in sideQuestListTransform)
            Destroy(item.gameObject);

        foreach (var task in QuestManager.Instance.questTasks)
        {
            var newTask = Instantiate(sideQuestNameBtn, sideQuestListTransform);
            newTask.SetUpNameBtn(task.questData);
        }
    }

    public void SetUpRequirements(QuestData_SO questData)
    {
        foreach (Transform item in requirementsTransform)
            Destroy(item.gameObject);

        foreach (var req in questData.questRequirements)
        {
            var newReq = Instantiate(questRequirement, requirementsTransform);
            if (questData.isFinished)
                newReq.SetUpRequirements(req.name, true);
            else newReq.SetUpRequirements(req.name, req.requiredAmount, req.currentAmount);
        }
    }

    public void SetUpRewardItem(ItemData_SO itemData, int amount)
    {
        var item = Instantiate(rewardUI, rewardsTransform);
        item.SetUpItemUI(itemData, amount);
    }
}