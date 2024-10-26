using UnityEngine;

public class TransitionDestination : MonoBehaviour
{
    public enum DestinationTag
    {
        BarEnter,
        BarExit,
        ChurchEnter,
        ChurchExit,
        DungeonEnter,
        DungeonExit,
        SecondFloorEnter,
        SecondFloorExit
    }

    public DestinationTag destinationTag;
}
