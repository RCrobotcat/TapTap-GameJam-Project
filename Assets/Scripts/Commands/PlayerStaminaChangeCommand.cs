using QFramework;

public class PlayerStaminaChangeCommand : AbstractCommand
{
    float _staminaChange;

    public PlayerStaminaChangeCommand(float staminaChange)
    {
        _staminaChange = staminaChange;
    }

    protected override void OnExecute()
    {
        var playerNumModel = this.GetModel<IPlayerNumModel>();

        playerNumModel.PlayerStaminaChange(_staminaChange);
    }
}
