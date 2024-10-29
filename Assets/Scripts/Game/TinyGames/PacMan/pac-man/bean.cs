using UnityEngine;
using UnityEngine.UI;

public class bean : MonoBehaviour
{
    public int BeanNum = 0;
    public float BlackNum = 0;
    public float count = 30;
    public Text time;
    public GameObject winTip;

    public float winGap;
    float timer;

    private void Awake()
    {
        isRespawn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bean")//吃豆功能和计数功能
        {
            Destroy(other.gameObject);
            AudioManager.Instance.PlaySfx(AudioManager.Instance.collectBean);
            BeanNum++;
            if (BlackNum < 0.68f)//屏幕逐级变暗数值
            {
                BlackNum = BlackNum + 0.017f;
            }
            count = count + 4;
        }
    }

    private void Update()
    {
        Win();
        decreasetime();
    }

    bool isWin;
    public void Win()//获得游戏胜利
    {
        if (BeanNum >= 20)
        {
            winTip.SetActive(true);
            if (timer < winGap)
            {
                timer += Time.deltaTime;
            }
            else
            {
                if (!isWin)
                {
                    isWin = true;
                    SceneController.Instance.HandleContinueGluttony(SaveManager.Instance.SceneName);
                }
            }
        }
    }

    bool isRespawn;
    public void decreasetime()
    {
        //time.text = count.ToString();
        if (count > 0)
        {
            count -= Time.deltaTime;
            time.text = count.ToString("0.00");
        }
        else if (count <= 0 && !isRespawn)
        {
            Debug.Log("you lose!");
            isRespawn = true;
            SceneController.Instance.HandleRespawnPacMan("Pac-Man-Map");
            //Time.timeScale = 0;
            //游戏失败结束
        }
    }
}
