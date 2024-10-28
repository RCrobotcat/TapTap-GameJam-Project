using UnityEngine;

public class DeadArea : MonoBehaviour
{
    public SceneFader sceneFaderPrefab;
    bool isDead;
    bool _intigger;

    private void Update()
    {
        if (_intigger && !isDead)
        {
            isDead = true;
            SceneController.Instance.HandleRespawnGoHundred("GoHundredScene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _intigger = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _intigger = false;
        }
    }
}