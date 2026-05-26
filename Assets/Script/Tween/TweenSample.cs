using UnityEngine;
using DG.Tweening;
using TMPro;


public class TweenSample : MonoBehaviour
{
    [Header("효과를 위한 UI/Object 타겟")]

    public RectTransform UITarget;      //UI 타겟
    public GameObject ObjectTarget;     //오브젝트 타겟 

    [Header("글자 연출 타겟")]
    public TMP_Text countText;
    public int currentValue = 0;
    public int addValue = 100;

    private int targetValue;

    [Header("색 변형 연출 예시")]
    public Color flashColor = Color.yellow;
    private Color originalColor;

    [Header("페이드 UI 그룹")]
    public CanvasGroup fadeTarget;


    
    void Start()
    {
        countText.text = currentValue.ToString();

        originalColor = countText.color;

        fadeTarget.alpha = 0;


    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayPunchUIScale();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayPunchObjectScale();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayUIShake();
        }


        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayCountUP();
        }


        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayColorFlash();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayFade();
        }
    }
    public void PlayPunchUIScale()
    {
        if (UITarget == null) return;            //없으면 리턴
        UITarget.DOKill();                       //이전 실행 중이던 Tween 효과가 있으면 정리한다.
        UITarget.localScale = Vector3.one;       //크기가 이상하게 남아 있으므로 기본 크기로 초기화
        UITarget.DOPunchScale(Vector3.one * 0.3f, 0.25f, 8, 1.01f);  //방향 , 크기 , 시간 , 진동 횟수 , 탄성
    }

    public void PlayPunchObjectScale()
    {
        if (ObjectTarget == null) return;
        ObjectTarget.transform.DOKill();
        ObjectTarget.transform.localScale = Vector3.one;
        ObjectTarget.transform.DOPunchScale(Vector3.one * 0.31f, 0.25f, 8, 1.0f);
    }

    public void PlayUIShake()
    {
        if (UITarget == null) return;
        UITarget.DOKill();
        UITarget.DOShakeAnchorPos(0.3f, 20f, 20, 90f);  //시간 , 강도 , 진동 횟수 , 랜덤성
    }

    public void PlayCountUP()
    {
        if (countText == null) return;

        targetValue += addValue;
        DOTween.Kill("CountTWeen", true);

        DOTween.To(
            () => currentValue,
            value =>
            {
                currentValue = value;
                countText.text = currentValue.ToString();

            },
            targetValue,
            0.5f
            )
            .SetEase(Ease.OutQuad)
            .SetId("Count");
     
        
    }

    public void PlayColorFlash()
    {
        if (countText == null) return;
        countText.DOKill();
        countText.color = originalColor;

        countText.DOColor(flashColor, 0.1f)
            .OnComplete(() =>
            {
                countText.DOColor(originalColor, 0.2f);
            });
    }
     public void PlayFade()
    {
        if (fadeTarget == null) return;
        fadeTarget.DOKill();
        fadeTarget.alpha = 0;

        Sequence seq = DOTween.Sequence();

        seq.Append(fadeTarget.DOFade(1f, 0.2f));
        seq.AppendInterval(0.5f);
        seq.Append(fadeTarget.DOFade(0f, 0.3f));

    }
}
