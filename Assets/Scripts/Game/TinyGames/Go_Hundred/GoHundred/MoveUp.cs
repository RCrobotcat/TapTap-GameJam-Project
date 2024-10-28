using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float moveSpeed;       //台阶平均速度
    public float currentSpeed;
    public float acceleration;    //加速度乘积

    public float DestroyDuraction;    //销毁时长
    public float DestroyCounter;

    public bool isGameOverStep;

    void Update()
    {
        //游戏开始时，给予速度加速度
        if (currentSpeed <= moveSpeed)
        {
            currentSpeed += Time.deltaTime * acceleration;
        }

        //向着指定方向移动
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
