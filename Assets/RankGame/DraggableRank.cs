using UnityEngine;

public class DraggableRank : MonoBehaviour
{
    public int rankLevel = 1;                        //계급장 레벨 (0은 빈칸)
    public float dragSpeed = 10f;                    //드래그 시 이동 속도
    public float snapBackSpeed = 20f;                //원 위치로 돌아가는 속도

    public bool isDragging = false;                  //현재 드래그 중인지 확인하는 변수
    public Vector3 originalPosition;                 //원래 위치
    public GridCell currentCell;                     //현재 위치한 칸

    public Camera mainCamera;                        //메인 카메라
    public Vector3 dragOffset;                       //드래그 시 오프셋 (보정값)
    public SpriteRenderer spriteRenderer;            //계급장 이미지 렌더러
    public RankGameManager gameManager;

    private void Awake()
    {
        //필요한 컴포넌트 참조 가져오기
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GetComponent<RankGameManager>();

    }


    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {

    }

     

    void StartDragging() //드래그 시작
    {
        isDragging = true;
        dragOffset = transform.position - GetMouseWorIdPosition();
        spriteRenderer.sortingOrder = 10;
    }

    public void MoveToCell(GridCell targetCell)
    {
        if (currentCell != null)

            currentCell.currentRank = null;


        currentCell = targetCell;
        targetCell.currentRank = this;

        originalPosition = new Vector3(targetCell.transform.position.x, targetCell.transform.position.y, 0f);
        transform.position = originalPosition;
    }

    public void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;    //원래 위치로 돌아가는 함수

    }
    public void MergeWithCell(GridCell targetCell)
    {
        if (targetCell.currentRank == null || targetCell.currentRank.rankLevel != rankLevel) //다른 레벨이거나 비어있다면
        {
            ReturnToOriginalPosition();
            return;
        }

        if (currentCell != null)
        {
            currentCell.currentRank = null;

        }
    }
    public Vector3 GetMouseWorIdPosition()
    {
        Vector3 mousePos = Input.mousePosition;               //마우스 월드 좌표 구하기
        mousePos.z = mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
    public void SetRankLevel(int level)
    {
        rankLevel = level;

        if (gameManager != null && gameManager.rankSprites.Length > level -1)
        {
            spriteRenderer.sprite = gameManager.rankSprites[level - 1];    //레벨에 맞는 스프라이트로 변경
        }
    }


}
