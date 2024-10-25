using UnityEngine;
using QFramework;

public interface IPlayerNumModel : IModel
{
    /*// Player Health
    BindableProperty<int> PlayerHealth { get; }*/

    // Player Stamina
    BindableProperty<float> PlayerStamina { get; }

    // Player Light
    BindableProperty<float> PlayerLight { get; }

    // void PlayerHealthChange(int changeVal);
    void PlayerStaminaChange(float changeVal);
    void PlayerLightChange(float changeVal);
}

public class PlayerNumModel : AbstractModel, IPlayerNumModel
{
    // public BindableProperty<int> PlayerHealth { get; } = new BindableProperty<int>(20);
    public BindableProperty<float> PlayerStamina { get; } = new BindableProperty<float>(15.0f);
    public BindableProperty<float> PlayerLight { get; } = new BindableProperty<float>(5.0f);

    /*public void PlayerHealthChange(int changeVal)
    {
        int currentHealth = PlayerHealth.Value + changeVal;
        PlayerHealth.Value = Mathf.Clamp(currentHealth, 0, 20);
    }*/

    public void PlayerStaminaChange(float changeVal)
    {
        float currentStamina = PlayerStamina.Value + changeVal;
        PlayerStamina.Value = Mathf.Clamp(currentStamina, 0, 15.0f);
    }

    public void PlayerLightChange(float changeVal)
    {
        float currentLight = PlayerLight.Value + changeVal;
        PlayerLight.Value = Mathf.Clamp(currentLight, 0, PlayerNumController.Instance.currentMaxLight);
    }

    protected override void OnInit() { }
}
