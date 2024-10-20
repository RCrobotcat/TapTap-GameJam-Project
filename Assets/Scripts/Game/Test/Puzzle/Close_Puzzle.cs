using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_Puzzle : MonoBehaviour
{
    private GameObject puzzles;

    private void Start()
    {
        puzzles = GameObject.Find("puzzle_panel");
        
    }

    // Update is called once per frame
    public void ClosePuzzlePanel()//关闭解密页面
    {
        puzzles.SetActive(false);
    }
}
