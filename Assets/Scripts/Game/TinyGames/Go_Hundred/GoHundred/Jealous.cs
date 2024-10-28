using Cinemachine;
using UnityEngine;

public class Jealous : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;
    public CreateStep gameOver;

    public float moveSpeed;

    private void Update()
    {
        if (transform.position.x < -4f)
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        GameOver();
    }

    public void GameOver()
    {
        if (gameOver.once == 1)
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * 2f * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x <= -15f)
            {
                Destroy(gameObject);
            }
        }
    }
    public void AttackPlane()
    {
        impulseSource.GenerateImpulse();
    }
}
