using UnityEngine;

public class FireOnOff : MonoBehaviour
{
    public GameObject fire;

    bool _inTrigger;

    void Update()
    {
        if (_inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            fire.SetActive(!fire.activeSelf);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inTrigger = false;
        }
    }
}
