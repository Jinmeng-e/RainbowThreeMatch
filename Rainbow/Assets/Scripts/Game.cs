using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockData
{
    public int x;
    public int y;
    public int colorIndex;
}
[System.Serializable]
public class HBlocks
{
    public int index;
    public Block[] blocks;

    public void Init(int index)
    {
        this.index = index;
        for(int i = 0; i < blocks.Length; ++i)
        {
            var y = i;
            blocks[i].SetPosition(index,y);
        }
    }
    public void Fill(int[] data)
    {
        for(int i = 0; i < blocks.Length;++i)
        {
            blocks[i].InitColor(data[i]);
        }
    }
    public void ShowDown()
    {

    }

    //debug
    public void CheckTest(int[] data)
    {
        for(int i = 0; i < blocks.Length; ++i)
        {
            if (data[i] == 0)
            {
                blocks[i].TestCheck();
            }
            else if(blocks[i].colorIndex != data[i])
            {
                blocks[i].TestChangeCheck();
            }
        }
    }
}
public class Game : MonoBehaviour
{
    public static Game instance;
    public int MaxColorCount => grade + 3;
    int grade = 0;
    int[,] boardData;
    public HBlocks[] blocks;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        grade = 0;
        boardData = new int[7, 7];
        for(int i = 0; i < 7; ++i)
        {
            for (int j = 0; j < 7; ++j)
            {
                boardData[i, j] = 0;
            }
        }
        for(int i = 0; i < blocks.Length; ++i)
        {
            blocks[i].Init(i);
        }
    }
    void Start()
    {
        FillData();
    }
    public bool Swap(BlockData a, BlockData b)
    {
        var temp = a.colorIndex;
        a.colorIndex = b.colorIndex;
        b.colorIndex = temp;

        boardData[a.x, a.y] = a.colorIndex;
        boardData[b.x, b.y] = b.colorIndex;
        

        return CheckMatch();
    }
    public void CheckTest()
    {
        Debug.Log("[Game] : TEST CHECK BLACK");
        if (CheckMatch())
        {
            ChangeColor();
        }
    }
    public void FillTest()
    {
        Debug.Log("[Game] : FILL MATCH BLOCK");

        FillData();
    }
    void ChangeColor()
    {
        var changed = new List<string>();
        for(var i = 0; i < 7; ++i)
        {
            var x = i;
            for (var j = 0; j < 7; ++j)
            {
                var y = j;

                if(boardData[x,y] == 0)
                {
                    if(x > 0 && boardData[x - 1, y] != 0)
                    {
                        if(!changed.Contains($"{x - 1}{y}"))
                        {
                            Debug.Log($"CHANGED :: {x - 1} : {y}");
                            changed.Add($"{x - 1}{y}");
                            boardData[x - 1, y] = ColorHelper.Instance.GetColorIndex(boardData[x - 1, y] + 1);
                        }
                    }
                    if(x < 6 && boardData[x + 1, y] != 0)
                    {
                        if (!changed.Contains($"{x + 1}{y}"))
                        {
                            Debug.Log($"CHANGED :: {x+1} : {y}");
                            changed.Add($"{x + 1}{y}");
                            boardData[x + 1, y] = ColorHelper.Instance.GetColorIndex(boardData[x + 1, y] + 1);
                        }
                    }
                    if(y > 0 && boardData[x , y - 1] != 0)
                    {
                        if (!changed.Contains($"{x}{y - 1}"))
                        {
                            Debug.Log($"CHANGED :: {x} : {y - 1}");
                            changed.Add($"{x}{y - 1}");
                            boardData[x, y - 1] = ColorHelper.Instance.GetColorIndex(boardData[x, y - 1] + 1);
                        }
                    }
                    if(y < 6 && boardData[x, y + 1] != 0)
                    {
                        if (!changed.Contains($"{x}{y + 1}"))
                        {
                            Debug.Log($"CHANGED :: {x} : {y + 1}");
                            changed.Add($"{x}{y + 1}");
                            boardData[x, y + 1] = ColorHelper.Instance.GetColorIndex(boardData[x, y + 1] + 1);
                        }
                    }
                }
            }
        }

        for (var i = 0; i < 7; ++i)
        {
            var data = new int[7];
            for (var j = 6; j >= 0; --j)
            {
                data[j] = boardData[i, j];
            }
            blocks[i].CheckTest(data);
        }
    }

    bool CheckMatch()
    {
        bool isMatched = false;

        // Check vertical matches

        var temp = new int[7,7];

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                temp[i,j] = boardData[i, j];
            }
        }
                

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                Debug.Log($"{i}{j} : {boardData[i, j]} : {boardData[i + 1, j]} : {boardData[i + 2, j]}");
                if (boardData[i, j] == boardData[i + 1, j] && boardData[i, j] == boardData[i + 2, j])
                {
                    temp[i, j] = 0;
                    temp[i + 1, j] = 0;
                    temp[i + 2, j] = 0;
                    isMatched = true;
                }
            }
        }
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 7; i++)
            {
                Debug.Log($"{i}{j} : {boardData[i, j]} : {boardData[i, j + 1]} : {boardData[i, j + 2]}");
                if (boardData[i, j] == boardData[i, j + 1] && boardData[i, j] == boardData[i, j + 2])
                {
                    temp[i, j] = 0;
                    temp[i, j + 1] = 0;
                    temp[i, j + 2] = 0;
                    isMatched = true;
                }
            }
        }

        boardData = temp;

        return isMatched;
    }

    void FillData()
    {
        Debug.Log($"[Game] : FillData");
        // 아래 번호가 비어 있으면 해당 줄 밑으로 당겨서 채움
        for (int i = 0; i < 7; ++i)
        {
            var data = new int[7];
            for (int j = 6; j >= 0; --j)
            {
                if (boardData[i, j] == 0)
                {
                    var index = j;
                    while(boardData[i,index] == 0 && index > 0)
                    {
                        --index;
                    }
                    if(boardData[i, index] > 0)
                    {
                        boardData[i, j] = boardData[i, index];
                        boardData[i, index] = 0;
                    }
                    else
                    {
                        var count = MaxColorCount;
                        boardData[i, j] = Random.Range(1, count+1);
                    }
                }
                data[j] = boardData[i, j];
                Debug.Log($"FILL : {i}{j} : {boardData[i, j]}");
            }
            blocks[i].Fill(data);
        }
    }
}
