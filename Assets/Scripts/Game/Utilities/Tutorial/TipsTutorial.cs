using Unity.VisualScripting;
using UnityEngine;

public class TipsTutorial : MonoBehaviour
{
    public GameObject tips1;
    public GameObject tips2;
    public GameObject tips3;
    public GameObject tips4;
    public GameObject tips5;
    public GameObject tips6;

    public GameObject weapon_basic;

    void Update()
    {
        // WASD
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            tips2.SetActive(true);

        // I
        if (Input.GetKeyDown(KeyCode.I))
            tips3.SetActive(true);

        // H
        if (Input.GetKeyDown(KeyCode.H))
        {
            tips4.SetActive(true);
            tips5.SetActive(true);
        }

        if (weapon_basic.IsDestroyed())
            tips6.SetActive(true);
    }
}
