using UnityEngine;

public class Interactive : MonoBehaviour
{
    public bool canPress;      //�жϽ�ɫ��ת����
    public Teleport teleport;   //��ȡ��ǰ�����Teleport�ű�

    void Update()
    {
        if (canPress && Input.GetKeyDown(KeyCode.F))
        {
            teleport.TriggerAction();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            canPress = true;
            teleport = other?.GetComponent<Teleport>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canPress = false;
    }
}
