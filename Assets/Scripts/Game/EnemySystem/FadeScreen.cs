using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeScreen : MonoBehaviour
{
    public Image fadeScreen;

    public  void ToFadeScreen(Color color, float duration)
    {
        //��Ļ��ɫת��
        fadeScreen.DOBlendableColor(color,duration);
    }
}
