using UnityEngine;
using DG.Tweening;

public class TweenCoin : MonoBehaviour
{
    
    void Start()
    {
        //코인이 생성 되었을 때 살짝 랜덤한 위치로 튀도록 목표 위치를 잡는다.
        Vector3 randomPosition = transform.position;
        new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

        //코인이 바닥에 떨어지는 것처럼 점프 이동한다
        //DOjump(목표 위치 , 점프 높이 , 점프 횟수 , 시간)
        transform.DOJump(randomPosition, 1.2f, 1, 0.4f).SetLink(gameObject);
        //SetLink는 오브젝트는 삭제될 때 tween도 같이 정리 되도록 도와 준다.

        //코인이 떨어질 때 한바퀴 회전
        transform.DORotate(new Vector3(0f, 360f, 0f), 0.4f, RotateMode.FastBeyond360).SetLink(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
