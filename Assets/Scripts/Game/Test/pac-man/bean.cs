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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bean")//吃豆功能和计数功能
        {
            Destroy(other.gameObject);
            BeanNum++;
            if (BlackNum < 0.68f)//屏幕逐级变暗数值
            {
                BlackNum = BlackNum + 0.017f;
            }
            count = count + 6f;
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
        if (BeanNum >= 30)
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

    public void decreasetime()
    {
        //time.text = count.ToString();
        if (count > 0)
        {
            count -= Time.deltaTime;
            time.text = count.ToString("0.00");
        }
        else if (count <= 0)
        {
            Debug.Log("you lose!");
            SceneController.Instance.HandleTransitionToScene("Pac-Man-Map");
            //Time.timeScale = 0;
            //游戏失败结束
        }
    }
}
