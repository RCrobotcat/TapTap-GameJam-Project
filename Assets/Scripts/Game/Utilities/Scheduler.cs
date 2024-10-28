using UnityEngine;

public class Scheduler : MonoBehaviour
{
    public GameObject Gluttony;

    void Update()
    {
        if (PlayerController.Instance.isGluttonyCompleted)
            Gluttony.SetActive(true);
        else Gluttony.SetActive(false);
    }
}
