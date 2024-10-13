using QFramework;

public class PlayerLightChangeCommand : AbstractCommand
{
    int _lightChange;

    public PlayerLightChangeCommand(int lightChange)
    {
        _lightChange = lightChange;
    }

    protected override void OnExecute()
    {
        var playerNumModel = this.GetModel<IPlayerNumModel>();
        playerNumModel.PlayerLightChange(_lightChange);
    }
}
