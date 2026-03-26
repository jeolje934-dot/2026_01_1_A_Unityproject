using UnityEngine;

public class MyBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)                      //유니티에서 지원해주는 충돌 체크 함수
    {
        Debug.Log(collision.gameObject + "와 충돌함");                    //충돌체의 이름을 기록한다

        if (collision . gameObject .tag == "Ground")                                //충돌이 일어난 물체의 Tag가 Ground 인 경우
        {
            Debug.Log("땅과 충돌");                                                 //로그가 기록 된다.
        }
    }

    void OnTriggerEnter(Collider other)                             //캐릭터가 특정지역을 들어갈 때(충돌범위) 체크 하는 경우
    {
        Debug.Log("트리거 안으로 들어옴");                                   //로그 기록을 쓴다
    }


    void OnTriggerExit(Collider other)                             
    {
        Debug.Log("트리거 밖으로 나감");                                    //로그 기록을 쓴다
    }
}
