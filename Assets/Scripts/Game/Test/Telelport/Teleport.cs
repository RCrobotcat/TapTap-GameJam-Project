using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameSceneSO gameScene;

    [Header("事件的广播")]
    public SceneLoadEventSO loadEventSO;
    public Vector3 positionToGo;

    public void TriggerAction()
    {
        loadEventSO.RaiseLoadRequestEvent(gameScene, positionToGo, true);
    }
}
