using UnityEngine;

public class TransitionToTinyGame : MonoBehaviour
{
    [Header("Transition Info")]
    public string sceneToTransit;

    public GameObject TipsText;

    bool _inTrigger;

    void Update()
    {
        if (_inTrigger && Input.GetKeyDown(KeyCode.T)
            && !PlayerController.Instance.combatWithEnemy)
            SceneController.Instance.HandleTransitionToScene(sceneToTransit);
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
