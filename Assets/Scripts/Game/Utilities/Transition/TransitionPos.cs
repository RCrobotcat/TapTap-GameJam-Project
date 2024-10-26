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
        if (_inTrigger && Input.GetKeyDown(KeyCode.T))
            SceneController.Instance.TransitionToDestination(sceneToTransit, destinationTag);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TipsText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _inTrigger = false;
        if (other.CompareTag("Player"))
            TipsText.SetActive(false);
    }
}
