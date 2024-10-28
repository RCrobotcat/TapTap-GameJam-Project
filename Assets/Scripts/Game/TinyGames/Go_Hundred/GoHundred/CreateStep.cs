using UnityEngine;

public class CreateStep : MonoBehaviour
{
    [Header("InstantiateGameObject")]
    public GameObject step_perfab;                    //生成物体
    public GameObject GameoverStep;                   //游戏结束生成台阶

    public float CreateObstructionDuraction;          //生成间隔

    public int Step_Counter = 0;                      //生成限制计数器
    public int Step_count;

    [HideInInspector] public float timer;
    public float RangeOffset;
    [HideInInspector] public int once = 0;

    private void Start()
    {
        timer = CreateObstructionDuraction;
    }
    private void Update()
    {
        if (timer <= CreateObstructionDuraction)
        {
            timer += Time.deltaTime;
        }
        else
        {
            //游戏已经结束
            GameOver();

            if (Step_count < Step_Counter)
            {
                CreateObstruction();
            }
        }
    }

    private void CreateObstruction()
    {
        timer = 0;
        Step_count++;
        Instantiate(step_perfab, new Vector3(transform.position.x, transform.position.y, transform.position.z + Random.Range(-RangeOffset, RangeOffset)), transform.rotation);
    }

    private void GameOver()
    {
        if (Step_count == Step_Counter && once != 1)
        {
            once++;
            Instantiate(GameoverStep, transform.position, transform.rotation);

            SceneController.Instance.HandleContinueJealous(SaveManager.Instance.SceneName);
        }
    }
}
