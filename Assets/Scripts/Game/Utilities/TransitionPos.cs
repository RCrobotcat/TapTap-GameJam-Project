using UnityEngine;

public class TransitionPos : MonoBehaviour
{
    public string sceneToTransit;

    public GameObject TipsText;

    bool _inTrigger;

    void Update()
    {
        if (_inTrigger && Input.GetKeyDown(KeyCode.T))
            StartCoroutine(SceneController.Instance.Transition(sceneToTransit));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TipsText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.T))
                StartCoroutine(SceneController.Instance.Transition(sceneToTransit));
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
