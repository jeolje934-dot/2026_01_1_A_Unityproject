
using UnityEngine;


public class FruitGame : MonoBehaviour
{

    public GameObject[] fruitPerfabs;             //과일 프리팹 배열 선언
    public float[] fruitSize = { 0.5f, 0.7f, 0.9f, 1.1f, 1.3f, 1.5f, 1.7f, 1.9f }; //과일 크기 선언
    public GameObject currentFruit;                       //현재 들고 있는 과일
    public int currentFruitType;                          //현재 들고 있는 과일 타입

    public float fruitStartHeigt = 6.0f;                     //과일 시작시 높이 설정
    public float gameWidth = 6.0f;                           //게임판 너비
    public bool isGameOver = false;                          //게임 상태
    public Camera mainCamera;                                //카메라 참조(마우스 위치 변환에 필요)

    //과일 생성 함수

    void SpawnnewFruit()
    {

        if (!isGameOver)                        //게임 오버가 아닐 때만 새 과일 생성
        {
            currentFruitType = Random.Range(0, 3);     //0 ~ 2 사이의 랜덤 과일 타입

            Vector3 mousePosition = Input.mousePosition;   //마우스 위치를 받아온다
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);    //마우스 2D 위치를 월드 3D 좌표로 변환

            Vector3 spawnPosition = new Vector3(worldPosition.x, fruitStartHeigt, 0); //X 좌표만 사용 하고 나머지는 설정한 값으로 진행 한다.

            float halfFruitSize = fruitSize[currentFruitType] / 2f;

            //X의 위치가 게임 영역을 벗어니자 않도록 제한
            spawnPosition.x = Mathf.Clamp(spawnPosition.x, -gameWidth / 2 + halfFruitSize, gameWidth / 2 - halfFruitSize);

            currentFruit = Instantiate(fruitPerfabs[currentFruitType], spawnPosition, Quaternion.identity);   //과일 생성
            currentFruit.transform.localScale = new Vector3(fruitSize[currentFruitType], fruitSize[currentFruitType], 1);  //과일 크기 설정

            Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.gravityScale = 0.0f;            //시작 시에는 중력 스케일을 0으로 해준다.

            }
        }
    }
    void Start()
    {
        mainCamera = Camera.main;          //메인 카메라 참조 가저오기
        SpawnnewFruit();                  //게임 시작 시 첫 과일 생성
    }
}
    
