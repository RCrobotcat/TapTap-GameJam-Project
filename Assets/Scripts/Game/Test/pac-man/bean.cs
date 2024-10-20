using UnityEngine;

public class bean : MonoBehaviour
{
    public int BeanNum = 0;
    public float BlackNum = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bean")//吃豆功能和计数功能
        {
            Destroy(other.gameObject);
            BeanNum++;
            if (BlackNum < 0.6f)
            {
                BlackNum = BlackNum + 0.3f;
            }
        }
    }

    private void Update()
    {
        if (BeanNum == 3)
        {
            Debug.Log("you win!");
        }
    }
}
