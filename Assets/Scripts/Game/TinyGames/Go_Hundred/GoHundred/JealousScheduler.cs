using UnityEngine;

public class JealousScheduler : MonoBehaviour
{
    public GameObject jealousFirstPhase;
    public GameObject jealousSecondPhase;

    private void Update()
    {
        if (PlayerController.Instance.isJealousCompleted)
        {
            jealousFirstPhase.SetActive(false);
            jealousSecondPhase.SetActive(true);
        }
        else
        {
            jealousFirstPhase.SetActive(true);
            jealousSecondPhase.SetActive(false);
        }
    }
}
