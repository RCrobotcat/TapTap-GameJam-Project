using QFramework;

public class PlayerApp : Architecture<PlayerApp>
{
    protected override void Init()
    {
        this.RegisterModel<IPlayerNumModel>(new PlayerNumModel());
        this.RegisterSystem<IPlayerNumSystem>(new PlayerNumSystem());
        this.RegisterUtility<Istorage>(new storage());
    }
}
