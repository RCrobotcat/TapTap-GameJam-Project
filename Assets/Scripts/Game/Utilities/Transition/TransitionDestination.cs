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
        SecondFloorExit,
        GardenEnter,
        GardenExit,
    }

    public DestinationTag destinationTag;
}
