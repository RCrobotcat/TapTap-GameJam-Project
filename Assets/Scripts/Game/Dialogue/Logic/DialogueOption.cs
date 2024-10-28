[System.Serializable] // can be shown in inspector
public class DialogueOption
{
    public string text;
    public string targetID;
    public bool takeQuest;
    public bool transitToScene;
    public string sceneName;
    public bool getItem;
    public ItemData_SO item;
}