using UnityEngine;

public class DialogueCtrl : MonoBehaviour
{
    public DialogueData_SO currentDialogue;
    bool canTalk = false;

    public GameObject TipsX;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentDialogue != null)
        {
            canTalk = true;
            TipsX.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = false;
            TipsX.SetActive(false);
            DialogueUI.Instance.dialoguePanel.SetActive(false);
        }
    }

    void Update()
    {
        if (canTalk && Input.GetKeyDown(KeyCode.X))
        {
            OpenDialogue();
            TipsX.SetActive(false);
        }
    }

    void OpenDialogue()
    {
        DialogueUI.Instance.UpdateDialogueData(currentDialogue); //, GetComponent<NPCController>());
        DialogueUI.Instance.UpdateMainDialogue(currentDialogue.dialoguePieces[0]);
    }
}