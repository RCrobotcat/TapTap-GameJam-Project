using UnityEngine;

public class Open_Puzzle : MonoBehaviour
{
    private GameObject puzzles;
    private bool puzzleActive = true;
    private void Start()
    {
        puzzles = GameObject.Find("puzzle_panel");
        puzzles.SetActive(false);
    }

    public void ispuzzle()
    {
        puzzleActive = false;
    }
    private void Update()
    {
        if (puzzleActive)              //点击打开解密页面
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.name == "puzzleObject")
                    {
                        puzzles.SetActive(true);
                    }
                }
            }
        }
    }
}