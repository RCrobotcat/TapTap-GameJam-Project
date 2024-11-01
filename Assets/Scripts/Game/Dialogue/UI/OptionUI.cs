using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Text optionText;
    Button btn;

    DialoguePiece currentPiece;

    string nextPieceID;
    bool takeQuest;
    bool followPlayer;
    bool transitToScene;
    string sceneName;
    bool getItem;
    ItemData_SO item;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnOptionClicked);
    }

    public void UpdateOption(DialoguePiece piece, DialogueOption option)
    {
        currentPiece = piece;
        optionText.text = option.text;
        nextPieceID = option.targetID;
        takeQuest = option.takeQuest;
        transitToScene = option.transitToScene;
        sceneName = option.sceneName;
        getItem = option.getItem;
        item = option.item;
    }

    public void OnOptionClicked()
    {
        if (currentPiece.questData != null)
        {
            var newTask = new QuestManager.QuestTask { questData = Instantiate(currentPiece.questData) };
            if (takeQuest)
            {
                // check if player already have the quest
                if (QuestManager.Instance.HaveQuest(newTask.questData))
                {
                    // the quest is completed and the player can finish the quest
                    if (QuestManager.Instance.GetQuestTask(newTask.questData).IsCompleted)
                    {
                        newTask.questData.GiveQuestRewards();
                        QuestManager.Instance.GetQuestTask(newTask.questData).IsFinished = true;
                        QuestUI.Instance.SetUpSideQuestList();
                    }
                }
                else
                {
                    QuestManager.Instance.questTasks.Add(newTask);
                    QuestUI.Instance.SetUpSideQuestList();
                    QuestManager.Instance.GetQuestTask(newTask.questData).IsStarted = true;

                    // Update the quest progress
                    foreach (var reqName in newTask.questData.GetRequiredTargetName())
                        InventoryManager.Instance.CheckQuestItemInBag(reqName);
                }
            }
        }

        /*if (followPlayer)
        {
            DialogueUI.Instance.currentNpc.followPlayer = true;
        }
        else
        {
            DialogueUI.Instance.currentNpc.followPlayer = false;
        }*/

        if (transitToScene)
        {
            SceneController.Instance.HandleTransitionToScene(sceneName);
        }

        if (getItem)
        {
            InventoryManager.Instance.inventoryData.AddItem(item, 1);
            InventoryManager.Instance.inventoryUI.RefreshUI();
            QuestManager.Instance.UpdateQuestProgress(item.name, 1);
        }

        if (nextPieceID == "")
        {
            DialogueUI.Instance.dialoguePanel.SetActive(false);
            return;
        }
        else
        {
            DialogueUI.Instance.UpdateMainDialogue(DialogueUI.Instance.currentDialogue.dialogueDict[nextPieceID]);
        }
    }
}