using UnityEngine;
using QFramework;

public interface Istorage : IUtility
{
    void SavePlayerNums(string key, float value);
    float LoadPlayerNums(string key);
}

public class storage : Istorage
{
    public void SavePlayerNums(string key, float value)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.SetFloat(key, value);
    }

    public float LoadPlayerNums(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }
}

