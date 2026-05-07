using System.Runtime.ConstrainedExecution;
using System.Security.Principal;
using UnityEngine;
using System.Collections.Generic;
using Mono.Cecil.Cil;

public class RankGameManager : MonoBehaviour
{
    public int gridWidth = 7;           //가로 칸수
    public int gridHeight = 7;          //세로 칸 수
    public float CellSize = 1.3f;       //각 칸의 크기
    public GameObject cellPrefabs;      //빈칸 프리팹
    public Transform gridContainer;     //그리드를 다음 부모 오브젝트

    public GameObject rankPrefabs;      //계급장 프리팹
    public Sprite[] rankSprites;        //각 레벨별 계급장 이미지
    public int maxRankLevel = 7;        //최대 계급장 레벨

    public GridCell[,] grid;            //모든 칸을 저장하는 2차원 배열



    void InitializeGrid()                                    //그리드 초기화
    {
        grid = new GridCell[gridWidth, gridHeight];          //2차원 배열 생성

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(
                x * CellSize - (gridWidth * CellSize / 2) + CellSize / 2,
                y * CellSize - (gridHeight * CellSize / 2) + CellSize / 2,
                1f
                );
                GameObject cell0bj = Instantiate(cellPrefabs, position, Quaternion.identity, gridContainer);
                GridCell cell = cell0bj.AddComponent<GridCell>();
                cell.Initialize(x, y);

                grid[x, y] = cell;   //배열에 저장
            }
        }
    }

    void Start()
    {
        InitializeGrid();

        for (int i = 0; i < 4; i++)             //4개의 계급장 생성
        {
            SpawnNewRank();
        }
    }

    void Update()
    {

    }


    public DraggableRank CreateRankCell(GridCell cell, int level)
    {
        if (cell == null && !cell.isEmpty()) return null;

        level = Mathf.Clamp(level, 1, maxRankLevel);

        Vector3 rankPosition = new Vector3(cell.transform.position.x, cell.transform.position.y, 0f);  //계급창 위치 설정

        //드래그 가능한 계급창 컴포넌트를 추가

        GameObject rankObj = Instantiate(rankPrefabs, rankPosition, Quaternion.identity, gridContainer);
        rankObj.name = "Rank_Level_" + level;

        DraggableRank rank = rankObj.AddComponent<DraggableRank>();

        rank.SetRankLevel(level);

        cell.SetRank(rank);

        return rank;
    }



    private GridCell FineEmptyCell()                            //비어 있는 칸 찾기
    {
        List<GridCell> emptyCells = new List<GridCell>();       //빈 칸들을 저장 할 리스트

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y].isEmpty())                                //칸이 비어 있다면 리스트에 추가
                {
                    emptyCells.Add(grid[x, y]);
                }
            }
        }
        if (emptyCells.Count == 0)                                       //빈 칸이 없으면 null 값 반환
        {
            return null;
        }

        return emptyCells[Random.Range(0, emptyCells.Count)];        //랜덤 하게 빈칸 하나 선택 
    }

    public bool SpawnNewRank()                                  //새 계급장 생성
    {
        GridCell empytCell = FineEmptyCell();                            //1. 빈칸 찾기
        if (empytCell == null) return false;                             //2. 빈칸 없으면 실패

        int rankLevel = Random.Range(0, 100) < 80 ? 1 : 2;               //80% 확률로 레벨 1 , 20% 확률로 2

        CreateRankCell(empytCell, rankLevel);                            //3. 계급장 생성 및 설정

        return true;
    }
}  

