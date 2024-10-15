using UnityEngine;

public class Interactive : MonoBehaviour
{
    public bool canPress;      //判断角色翻转方向
    public Teleport teleport;   //获取当前物体的Teleport脚本

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
