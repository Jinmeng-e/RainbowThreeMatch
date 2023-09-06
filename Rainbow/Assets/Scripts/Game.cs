using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BlockData
{
    public int x;
    public int y;
    public int colorIndex;
    public int dropHeight;
}
[System.Serializable]
public class ScoreData
{
    public int count;
    public int score;
}
[System.Serializable]
public class HBlocks
{
    public int index;
    public Block[] blocks;

    public void Init(int index)
    {
        this.index = index;
        var yCount = PositionHelper.instance.YCount;
        for (int i = 0; i < yCount; ++i)
        {
            var y = i;
            blocks[i].SetPosition(index,y);
        }
    }
    public void FillData(BlockData[] data)
    {
        var yCount = PositionHelper.instance.YCount;
        for (int i = 0; i < yCount;++i)
        {
            var index = data[i].colorIndex;
            var height = data[i].dropHeight;
            blocks[i].InitColorData(index);
            blocks[i].Show(height);
        }
    }

    internal void Pop(BlockData[] data)
    {
        for(int i = 0; i < data.Length; ++i)
        {
            if(data[i].colorIndex == 0)
            {
                blocks[i].Pop();
            }
        }
    }
    internal void ChangeColor(int[] data)
    {
        var yCount = PositionHelper.instance.YCount;
        for (int i = 0; i < yCount; ++i)
        {
            if (data[i] != 0 && blocks[i].colorIndex != data[i])
            {
                blocks[i].InitColorData(data[i]);
                //blocks[i].ChangeColor();
                blocks[i].ChangeSprite();
            }
        }
    }

    //debug
    public void CheckTest(int[] data)
    {
        var yCount = PositionHelper.instance.YCount;
        for (int i = 0; i < yCount; ++i)
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
    [SerializeField] Image screen;
    [SerializeField] GameObject alphaScreen;
    [SerializeField] GameObject objGameOver;

    [SerializeField] int grade;
    [SerializeField] int timeSeconds;
    public float DropTime = 1f;
    int[,] boardData;
    public HBlocks[] blocks;
    int score;
    public System.Action<int> ShowScore;
    public System.Action<int> ShowTimer;
    public int Score
    {
        get { return score;}
        set {
            if (score == value) return;
            score = value;
            ShowScore?.Invoke(score);
        }
    }
    public int TimeSeconds
    {
        get { return time; }
        set
        {
            if (time == value) return;
            time = value;
            ShowTimer?.Invoke(time);
        }
    }
    int time;
    int maxHeight;

    private void Awake()
    {
        var xCount = PositionHelper.instance.XCount;
        var yCount = PositionHelper.instance.YCount;
        score = 0;
        if(instance == null)
        {
            instance = this;
        }
        boardData = new int[xCount, yCount];
        for(int i = 0; i < xCount; ++i)
        {
            for (int j = 0; j < yCount; ++j)
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
        alphaScreen.SetActive(true);
        OnClickRetry();
    }
    public void Check((int,int) block)
    {
        Debug.Log($"[Game] : CHECK BLACK :: {block.Item1}, {block.Item2}");

        alphaScreen.SetActive(true);
        if (CheckMatch(block))
        {
            StartCoroutine(IEPop());
        }
        else
        {
            alphaScreen.SetActive(false);
        }
    }
    public void CheckTest()
    {
        Debug.Log("[Game] : TEST CHECK BLACK");
        if (CheckMatch())
        {
            ChangeColorTest();
        }
    }
    public void FillTest()
    {
        Debug.Log("[Game] : FILL MATCH BLOCK");

        FillData();
    }
    public void OnClickRetry()
    {
        StartCoroutine(IEStartGame());
    }
    
    void ChangeColorTest()
    {
        Debug.Log($"[GAME] : ChangeColor ::::::::::");


        var xCount = PositionHelper.instance.XCount;
        var yCount = PositionHelper.instance.YCount;
        var changed = new List<string>();
        for(var i = 0; i < xCount; ++i)
        {
            var x = i;
            for (var j = 0; j < yCount; ++j)
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
                    if(x < 4 && boardData[x + 1, y] != 0)
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

        for (var i = 0; i < xCount; ++i)
        {
            var data = new int[yCount];
            for (var j = 6; j >= 0; --j)
            {
                data[j] = boardData[i, j];
            }
            blocks[i].CheckTest(data);
        }
    }
    void ChangeColor()
    {
        var xCount = PositionHelper.instance.XCount;
        var yCount = PositionHelper.instance.YCount;
        Debug.Log($"[GAME] : ChangeColor ::::::::::");
        var changed = new List<string>();
        for (var i = 0; i < xCount; ++i)
        {
            var x = i;
            for (var j = 0; j < yCount; ++j)
            {
                var y = j;

                if (boardData[x, y] == 0)
                {
                    if (x > 0 && boardData[x - 1, y] != 0)
                    {
                        if (!changed.Contains($"{x - 1}{y}"))
                        {
                            Debug.Log($"CHANGED :: {x - 1} : {y}");
                            changed.Add($"{x - 1}{y}");
                            boardData[x - 1, y] = ColorHelper.Instance.GetColorIndex(boardData[x - 1, y] + 1);
                        }
                    }
                    if (x < xCount-1 && boardData[x + 1, y] != 0)
                    {
                        if (!changed.Contains($"{x + 1}{y}"))
                        {
                            Debug.Log($"CHANGED :: {x + 1} : {y}");
                            changed.Add($"{x + 1}{y}");
                            boardData[x + 1, y] = ColorHelper.Instance.GetColorIndex(boardData[x + 1, y] + 1);
                        }
                    }
                    if (y > 0 && boardData[x, y - 1] != 0)
                    {
                        if (!changed.Contains($"{x}{y - 1}"))
                        {
                            Debug.Log($"CHANGED :: {x} : {y - 1}");
                            changed.Add($"{x}{y - 1}");
                            boardData[x, y - 1] = ColorHelper.Instance.GetColorIndex(boardData[x, y - 1] + 1);
                        }
                    }
                    if (y < yCount-1 && boardData[x, y + 1] != 0)
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

        for (var i = 0; i < xCount; ++i)
        {
            var data = new int[yCount];
            for (var j = yCount-1; j >= 0; --j)
            {
                data[j] = boardData[i, j];
            }
            blocks[i].ChangeColor(data);
        }
    }

    bool CheckMatch()
    {
        var xCount = PositionHelper.instance.XCount;
        var yCount = PositionHelper.instance.YCount;
        bool isMatched = false;

        // Check vertical matches

        var temp = new int[xCount,yCount];

        for (int i = 0; i < xCount; i++)
        {
            for (int j = 0; j < yCount; j++)
            {
                temp[i,j] = boardData[i, j];
            }
        }

        List<List<(int, int)>> connectedComponents = new List<List<(int, int)>>();

        for (int i = 0; i < xCount; ++i)
        {
            for (int j = 0; j < yCount; ++j)
            {
                if (boardData[i, j] != 0)
                {
                    List<(int, int)> component = new List<(int, int)>();
                    DFS(boardData[i,j], i, j, ref component);
                    connectedComponents.Add(component);
                }
            }
        }


        foreach (var component in connectedComponents)
        {
            if (component.Count >= 3)
            {
                isMatched = true;
                foreach (var item in component)
                {
                    boardData[item.Item1, item.Item2] = 0;
                }
            }
        }


        for (int i = 0; i < xCount; i++)
        {
            BlockData[] data = new BlockData[yCount];

            for (int j = yCount-1; j >= 0; j--)
            {
                BlockData block = new BlockData();
                block.x = i;
                block.y = j;
                block.colorIndex = boardData[i, j];
                block.dropHeight = 0;
                data[j] = block;
            }
            blocks[i].Pop(data);
        }


        // line matching
        //for (int i = 0; i < 3; i++)
        //{
        //    for (int j = 0; j < 7; j++)
        //    {
        //        Debug.Log($"{i}{j} : {boardData[i, j]} : {boardData[i + 1, j]} : {boardData[i + 2, j]}");
        //        if (boardData[i, j] == boardData[i + 1, j] && boardData[i, j] == boardData[i + 2, j])
        //        {
        //            temp[i, j] = 0;
        //            temp[i + 1, j] = 0;
        //            temp[i + 2, j] = 0;
        //            isMatched = true;
        //        }
        //    }
        //}
        //for (int j = 0; j < 5; j++)
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        Debug.Log($"{i}{j} : {boardData[i, j]} : {boardData[i, j + 1]} : {boardData[i, j + 2]}");
        //        if (boardData[i, j] == boardData[i, j + 1] && boardData[i, j] == boardData[i, j + 2])
        //        {
        //            temp[i, j] = 0;
        //            temp[i, j + 1] = 0;
        //            temp[i, j + 2] = 0;
        //            isMatched = true;
        //        }
        //    }
        //}

        //boardData = temp;

        //for (int i = 0; i < 5; i++)
        //{
        //    BlockData[] data = new BlockData[7];

        //    for (int j = 6; j >= 0; j--)
        //    {
        //        BlockData block = new BlockData();
        //        block.x = i;
        //        block.y = j;
        //        block.colorIndex = boardData[i, j];
        //        block.dropHeight = 0;
        //        data[j] = block;
        //    }
        //    blocks[i].Pop(data);
        //}


        return isMatched;
    }

    bool CheckMatch((int,int) blockIndex)
    {
        var xCount = PositionHelper.instance.XCount;
        var yCount = PositionHelper.instance.YCount;
        bool isMatched = false;

        // Check vertical matches

        var temp = new int[xCount, yCount];

        for (int i = 0; i < xCount; i++)
        {
            for (int j = 0; j < yCount; j++)
            {
                temp[i, j] = boardData[i, j];
            }
        }

        List<(int, int)> component = new List<(int, int)>();
        DFS(boardData[blockIndex.Item1, blockIndex.Item2], blockIndex.Item1, blockIndex.Item2, ref component);

        if (component.Count >= 3)
        {
            isMatched = true;
            foreach (var item in component)
            {
                boardData[item.Item1, item.Item2] = 0;
            }
            AddScores(component.Count);
        }


        for (int i = 0; i < xCount; i++)
        {
            BlockData[] data = new BlockData[yCount];

            for (int j = yCount-1; j >= 0; j--)
            {
                BlockData block = new BlockData();
                block.x = i;
                block.y = j;
                block.colorIndex = boardData[i, j];
                block.dropHeight = 0;
                data[j] = block;
            }
            blocks[i].Pop(data);
        }

        return isMatched;
    }
    void DFS(int matchValue, int i, int j, ref List<(int, int)> component)
    {
        var xCount = PositionHelper.instance.XCount;
        var yCount = PositionHelper.instance.YCount;
        if (i < 0 || i >= xCount || j < 0 || j >= yCount || boardData[i, j] <= 0 || component.Contains((i, j)) || boardData[i, j] != matchValue)
        {
            return;
        }

        component.Add((i, j));
        int value = boardData[i, j];

        DFS(matchValue, i - 1, j, ref component);
        DFS(matchValue, i + 1, j, ref component);
        DFS(matchValue, i, j - 1, ref component);
        DFS(matchValue, i, j + 1, ref component);
    }
    void FillData()
    {
        Debug.Log("[Game] : FillData");
        var xCount = PositionHelper.instance.XCount;
        var yCount = PositionHelper.instance.YCount;

        int maxColorCount = MaxColorCount;

        for (int i = 0; i < xCount; i++)
        {
            BlockData[] data = new BlockData[yCount];
            var dropCount = 0;
            for (int j = yCount-1; j >= 0; j--)
            {
                if (boardData[i, j] == 0) // when empty
                {
                    int index = j;

                    while (index >= 0 && boardData[i, index] == 0) // back up to empty index
                    {
                        index--;
                        dropCount++;
                    }

                    if (index >= 0 && boardData[i, index] > 0) // Drop from top to bottom if the top is filled with something
                    {
                        for (int dropIndex = index; dropIndex >= 0; dropIndex--)
                        {
                            boardData[i, j - (index - dropIndex)] = boardData[i, dropIndex];
                        }

                        for (int others = j - (index + 1); others >= 0; others--)
                        {
                            boardData[i, others] = Random.Range(1, maxColorCount + 1);
                        }
                    }
                    else
                    {
                        boardData[i, j] = Random.Range(1, maxColorCount + 1);
                    }
                }

                BlockData block = new BlockData();
                block.x = i;
                block.y = j;
                block.colorIndex = boardData[i, j];
                block.dropHeight = dropCount;
                data[j] = block;

                if (maxHeight < dropCount) { maxHeight = dropCount; }
                Debug.Log($"FILL: {i}, {j}: {boardData[i, j]} :: {dropCount}");
            }

            blocks[i].FillData(data);
        }

        if(maxHeight > 0)
        {
            StartCoroutine(IEDrop(maxHeight));
        }
    }
    void InitData()
    {
        Debug.Log("[GAME] : InitData()");

        var xCount = PositionHelper.instance.XCount;
        var yCount = PositionHelper.instance.YCount;

        var count = MaxColorCount;
        for (var i = 0; i < xCount; ++i)
        {
            var data = new BlockData[yCount];

            for (var j = yCount-1; j >= 0; --j)
            {
                boardData[i, j] = Random.Range(1, count + 1);

                var block = new BlockData();
                block.x = i;

                block.y = j;
                block.colorIndex = boardData[i, j];
                block.dropHeight = 7;
                data[j] = block;
            }
            blocks[i].FillData(data);
        }

        StartCoroutine(IEDrop(maxHeight));
    }
    void AddScores(int count)
    {

        var value = count - 3;
        if(value < 0) { return; }
        var added = 0;
        for (int i = value; i > 0; --i)
        {
            added += value;
        }
        
        Score = score + 10 + added;
        Debug.Log($"[GAME] : Current Added Score : Count '{count}' :: AddedScore :: {10 + added}");
    }
    IEnumerator IEStartGame()
    {
        Debug.Log("IEStartGame()");

        yield return new WaitUntil(() => ColorHelper.Instance.IsColorFilled);
        Debug.Log("IEStartGame()");
        maxHeight = 7;
        InitData();
        yield return null;
        Debug.Log("IEStartGame()");
        screen.gameObject.SetActive(false);
        objGameOver.SetActive(false);
        TimeSeconds = timeSeconds;
        Score = 0;
        yield return StartCoroutine(IETimer());
    }

    IEnumerator IEDrop(int height)
    {
        // 1 - 7  // 0.875 - 0.125
        // set height
        var HValue = (float)(8 - height) * 0.125f;

        // animation
        float timeValue = HValue * DropTime;
        //Debug.Log($"[Block] : HEIGHT :: {height} :: HVALUE :: {HValue} :: timevalue {timeValue}");

        while (DropTime > timeValue)
        {
            timeValue += Time.deltaTime;
            if (timeValue >= DropTime) { timeValue = DropTime; }
            yield return null;
        }

        maxHeight = 0;
        alphaScreen.SetActive(false);
        yield return null;
    }
    IEnumerator IEPop()
    {
        yield return null;

        ChangeColor();
        yield return new WaitForSeconds(0.5f);

        FillData();
    }
    IEnumerator IETimer()
    {
        Debug.Log($"IETimer() {TimeSeconds}");
        while (TimeSeconds > 0)
        {
            yield return new WaitForSeconds(1f);
            TimeSeconds--;
            Debug.Log($"Remain :: {TimeSeconds}");
        }
        objGameOver.SetActive(true);
    }
}