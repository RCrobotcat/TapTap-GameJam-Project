using TMPro;
using UnityEngine;

public class Solve_Puzzles : MonoBehaviour
{
    public Open_Puzzle open_Puzzle;
    public TextMeshProUGUI num1;
    public TextMeshProUGUI num2;
    public TextMeshProUGUI num3;
    public TextMeshProUGUI num4;
    int i = 0;
    int j = 0;
    int k = 0;
    int l = 0;

    private GameObject puzzles;

    private void Start()
    {
        puzzles = GameObject.Find("puzzle_panel");

    }
    public void Num1()//密码输入
    {
        if (i < 9)
        {
            i++;
            num1.text = i.ToString();
        }
        else
        {
            i = 0;
        }
    }

    public void Num2()
    {
        if (j < 9)
        {
            j++;
            num2.text = j.ToString();
        }
        else
        {
            j = 0;
        }
    }

    public void Num3()
    {
        if (k < 9)
        {
            k++;
            num3.text = k.ToString();
        }
        else
        {
            k = 0;
        }
    }

    public void Num4()
    {
        if (l < 9)
        {
            l++;
            num4.text = l.ToString();
        }
        else
        {
            l = 0;
        }
    }

    public void openpuzzle()  //密码设置和解开密码
    {
        if (i == 1 && j == 2 && k == 3 && l == 4)
        {
            Debug.Log("你已经完成解密");
            puzzles.SetActive(false);
            open_Puzzle.ispuzzle();
        }
        else
        {
            Debug.Log("你没有完成解密");
        }
    }
}
