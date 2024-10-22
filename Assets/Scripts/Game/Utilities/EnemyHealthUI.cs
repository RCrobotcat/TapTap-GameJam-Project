using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyHealthUI : MonoBehaviour
{
    public Image EnemyHealthSlider;
    public Text EnemyHealthText;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float fillAmount = currentHealth / maxHealth;
        EnemyHealthSlider.DOFillAmount(fillAmount, 0.5f);
        EnemyHealthText.text = $"{(int)currentHealth}/{(int)maxHealth}";
    }
}
