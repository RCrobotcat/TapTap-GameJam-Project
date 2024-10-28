using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float moveSpeed;       //̨��ƽ���ٶ�
    public float currentSpeed;
    public float acceleration;    //���ٶȳ˻�

    public float DestroyDuraction;    //����ʱ��
    public float DestroyCounter;

    public bool isGameOverStep;

    void Update()
    {
        //��Ϸ��ʼʱ�������ٶȼ��ٶ�
        if (currentSpeed <= moveSpeed)
        {
            currentSpeed += Time.deltaTime * acceleration;
        }

        //����ָ�������ƶ�
        transform.Translate(-Vector3.right * currentSpeed * Time.deltaTime);

        DestroyGameObject();

    }

    private void DestroyGameObject()
    {
        if (isGameOverStep)
        {
            if (transform.position.x <= -20f)
            {
                currentSpeed = 0;
            }
        }
        else
        {
            if (DestroyCounter >= DestroyDuraction)
            {
                Destroy(gameObject);
            }
            else
            {
                DestroyCounter += Time.deltaTime;
            }
        }
    }
}
