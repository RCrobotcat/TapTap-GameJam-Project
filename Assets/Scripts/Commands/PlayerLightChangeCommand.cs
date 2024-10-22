using QFramework;

public class PlayerLightChangeCommand : AbstractCommand
{
    float _lightChange;

    public PlayerLightChangeCommand(float lightChange)
    {
        _lightChange = lightChange;
    }

    protected override void OnExecute()
    {
        var playerNumModel = this.GetModel<IPlayerNumModel>();
        playerNumModel.PlayerLightChange(_lightChange);
    }
}
