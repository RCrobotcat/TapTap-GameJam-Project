using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeScreen : MonoBehaviour
{
    public Image fadeScreen;

    public void ToFadeScreen(Color color, float duration)
    {
        //ÆÁÄ»ÑÕÉ«×ª±ä
        fadeScreen.DOBlendableColor(color, duration);
    }
}
