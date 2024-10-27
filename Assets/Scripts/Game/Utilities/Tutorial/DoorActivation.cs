using UnityEngine;

public class DoorActivation : MonoBehaviour
{
    public GameObject door;

    void Update()
    {
        if (QuestManager.Instance.questTasks.Count > 0)
        {
            if (QuestManager.Instance.questTasks[0].questData.name == "LustElimination(Clone)")
            {
                door.SetActive(true);
            }
        }
    }
}
