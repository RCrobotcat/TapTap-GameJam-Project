using QFramework;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerNumController : Singleton<PlayerNumController>, IController
{
    /*public Transform PlayerHealthBar;
    Image HealthSlider;*/

    public Transform PlayerStaminaBar;
    Image StaminaSlider;

    public Transform PlayerLightBar;
    Image LightSlider;

    [Header("Player Nums Settings")]
    public float RunStaminaCost;
    public float RageLightCost;

    public IPlayerNumModel mModel;

    protected override void Awake()
    {
        base.Awake();

        // HealthSlider = PlayerHealthBar.GetChild(0).GetComponent<Image>();
        StaminaSlider = PlayerStaminaBar.GetChild(0).GetComponent<Image>();
        LightSlider = PlayerLightBar.GetChild(0).GetComponent<Image>();
    }

    void Start()
    {
        mModel = this.GetModel<IPlayerNumModel>();

        /*mModel.PlayerHealth.RegisterWithInitValue(health =>
        {
            UpdateHealthBar();
        }).UnRegisterWhenGameObjectDestroyed(gameObject);*/

        mModel.PlayerStamina.RegisterWithInitValue(stamina =>
        {
            UpdateStaminaBar();
        }).UnRegisterWhenGameObjectDestroyed(gameObject);

        mModel.PlayerLight.RegisterWithInitValue(light =>
        {
            UpdateLightBar();
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    void Update()
    {
        HandleStaminaChange();

        HandleLightChange();
    }

    void HandleLightChange()
    {
        if (PlayerController.Instance.equipRage)
        {
            float cost = RageLightCost * Time.deltaTime * -1f;
            this.SendCommand(new PlayerLightChangeCommand(cost));
            if(mModel.PlayerLight.Value <= 0.5f)
            {
                PlayerController.Instance.equipRage = false;
            }
        }
    }

    void HandleStaminaChange()
    {
        if (PlayerController.Instance.isRunning && PlayerController.Instance.agent.velocity.magnitude > 0.1f)
        {
            float cost = RunStaminaCost * Time.deltaTime * -1f;
            this.SendCommand(new PlayerStaminaChangeCommand(cost));
        }
        else
        {
            float add = RunStaminaCost * Time.deltaTime;
            this.SendCommand(new PlayerStaminaChangeCommand(add));
        }
    }

    /*void UpdateHealthBar()
    {
        float SliderPercent = (float)mModel.PlayerHealth.Value / 20;
        HealthSlider.DOFillAmount(SliderPercent, 0.3f);
    }*/

    void UpdateStaminaBar()
    {
        float SliderPercent = (float)mModel.PlayerStamina.Value / 15.0f;
        StaminaSlider.DOFillAmount(SliderPercent, 0.3f);
    }

    void UpdateLightBar()
    {
        float SliderPercent = (float)mModel.PlayerLight.Value / 5.0f;
        LightSlider.DOFillAmount(SliderPercent, 0.3f);
    }

    public void StaminaChange(float val)
    {
        this.SendCommand(new PlayerStaminaChangeCommand(val));
    }

    public IArchitecture GetArchitecture()
    {
        return PlayerApp.Interface;
    }
}
