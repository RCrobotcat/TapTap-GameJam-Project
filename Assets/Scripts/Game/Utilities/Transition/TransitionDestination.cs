using UnityEngine;

public class TransitionDestination : MonoBehaviour
{
    public enum DestinationTag
    {
        BarEnter, BarExit, ChurchEnter, ChurchExit, DungeonEnter, DungeonExit
    }

    public DestinationTag destinationTag;
}
