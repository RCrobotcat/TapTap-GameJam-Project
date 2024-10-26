using UnityEngine;

public class TransitionPos : MonoBehaviour
{
    [Header("Transition Info")]
    public string sceneToTransit;
    public TransitionDestination.DestinationTag destinationTag;

    public GameObject TipsText;

    bool _inTrigger;

    void Update()
    {
        if (_inTrigger && Input.GetKeyDown(KeyCode.T)
            && !PlayerController.Instance.combatWithEnemy)
            SceneController.Instance.TransitionToDestination(sceneToTransit, destinationTag);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PlayerController.Instance.combatWithEnemy)
        {
            TipsText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            _inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            _inTrigger = false;
            TipsText.SetActive(false);
        }
    }
}
