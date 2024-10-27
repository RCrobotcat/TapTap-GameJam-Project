using QFramework;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class PlayerNumController : Singleton<PlayerNumController>, IController
{
    /*public Transform PlayerHealthBar;
    Image HealthSlider;*/

    public Transform PlayerStaminaBar;
    Image StaminaSlider;

    public Transform PlayerLightBar;
    Image LightSlider;
    public Text lightTxt;
    public Text lightAddTxt;

    [Header("Player Nums Settings")]
    public float RunStaminaCost;
    public float RageLightCost;
    public float LightAddition; // Light addition when player complete a quest
    [HideInInspector] public float currentMaxLight = 5.0f;

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
            if (mModel.PlayerLight.Value <= 0.5f)
            {
                PlayerController.Instance.equipRage = false;
            }
        }
        /*else if(!PlayerController.Instance.equipRage)
        {
            float add = RageLightCost * Time.deltaTime;
            this.SendCommand(new PlayerLightChangeCommand(add));
        }*/
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
        float SliderPercent = (float)mModel.PlayerLight.Value / currentMaxLight;
        LightSlider.DOFillAmount(SliderPercent, 0.3f);
        lightTxt.text = mModel.PlayerLight.Value.ToString("F1") + "/" + currentMaxLight.ToString("F1");
    }

    public void StaminaChange(float val)
    {
        this.SendCommand(new PlayerStaminaChangeCommand(val));
    }

    // Update the player max Light
    public void MaxLightUpdate()
    {
        currentMaxLight += LightAddition;
        float lightAddScale = LightAddition / currentMaxLight;
        Vector3 endScale = new Vector3(1 + lightAddScale, 1, 1);
        Vector3 originalPos = PlayerLightBar.position;
        Vector3 endPosition = originalPos + new Vector3(20, 0, 0);
        PlayerLightBar.DOMove(endPosition, 0.5f);
        PlayerLightBar.DOScale(endScale, 0.5f);
        UpdateLightBar();
        lightAddTxt.gameObject.SetActive(true);
        SavePlayerNums();
        StartCoroutine(DeactivateLightAddTxt());
    }

    IEnumerator DeactivateLightAddTxt()
    {
        yield return new WaitForSeconds(2.0f);
        lightAddTxt.gameObject.SetActive(false);
    }

    #region Save/Load Player Nums
    public void SavePlayerNums()
    {
        var Storage = this.GetUtility<Istorage>();
        Storage.SavePlayerNums("PlayerMaxLight", currentMaxLight);
        Storage.SavePlayerNums("PlayerLight", mModel.PlayerLight.Value);
        Debug.Log("Player Nums Saved!");
    }

    public void LoadPlayerNums()
    {
        var Storage = this.GetUtility<Istorage>();
        currentMaxLight = Storage.LoadPlayerNums("PlayerMaxLight");
        mModel.PlayerLight.Value = Storage.LoadPlayerNums("PlayerLight");
    }
    #endregion

    public IArchitecture GetArchitecture()
    {
        return PlayerApp.Interface;
    }
}
