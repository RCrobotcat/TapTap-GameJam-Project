using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameSceneSO gameScene;

    [Header("�¼��Ĺ㲥")]
    public SceneLoadEventSO loadEventSO;
    public Vector3 positionToGo;

    public void TriggerAction()
    {
        loadEventSO.RaiseLoadRequestEvent(gameScene, positionToGo, true);
    }
}
