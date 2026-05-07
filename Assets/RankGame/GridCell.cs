using UnityEngine;



public class GridCell : MonoBehaviour
{
    public int x, y;                          //그리드에서의 위치(좌표)
    public DraggableRank currentRank;       //현재 칸에 있는 계급창
    public SpriteRenderer cellRenderers;      //칸에 이미지 렌더러

    //좌표 초기화
    public void Initialize(int gridX , int gridY)
    {
        x = gridX;
        y = gridY;
        name = "Cell_" + x + "_" + y;      //이름 설정

    }
     
    public bool isEmpty()
    {
        return currentRank == null;
    }

    public bool ContainPosition(Vector3 position)
    {
        Bounds bounds = cellRenderers.bounds;
        return bounds.Contains(position);
    }

    public void SetRank(DraggableRank rank)     //칸에 계급장 설정
    {
        currentRank = rank;

        if(rank != null)
        {
            rank.currentCell = this;
        }

        rank.originalPosition = new Vector3(transform.position.x, transform.position.y, 0);  //z의 위치를 0으로 설정(2D)
        rank.transform.position = new Vector3(transform.position.x, transform.position.y, 0); //계급장 현재 칸 위치로 이동
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
