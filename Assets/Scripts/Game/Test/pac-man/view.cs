using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class view : MonoBehaviour
{
    // Start is called before the first frame update
    public bean bean;
    private int vnum;
    public GameObject dackpanel;
    public Image image;
    private float time = 1f;
    private float numcolor = 0f;
    private void Start()
    {
        image.color = new Color(0, 0, 0, 0.3f);
    }
    private void Update()
    {
        vnum = bean.BeanNum;
        NumIncrement();
        Black();
    }


    private void Black()//屏幕变黑
    {
        if (vnum >= 1)
        {
            image.color = new Color(0,0,0,numcolor);
        }
    }

    private void NumIncrement()//黑暗数值动态变化值
    {
        numcolor += ((0.3f + bean.BlackNum) / time) * Time.deltaTime;
        if(numcolor >= (0.3f + bean.BlackNum))
        {
            numcolor = (0.3f + bean.BlackNum);
        }
    }
}
