using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyStatus : MonoBehaviour
{ // 클래스와 상속을 이용해서 다시 짤 예정
    public float health; // 체력
    public float damage; // 공격력
    public float defend; // 방어력

    public CatStatus catstatus;
    public Stage stage;
    private Animator animator;

    private float jellyTempHealth;

    public int jellyCount; // 현재 젤리의 순서

    public bool life;

    void Start()
    {
    }

    void Update()
    {
        if (health <= 0 && life == true) // 젤리맨이 죽었을 때 실행
        {
            StartCoroutine(DeadLoop());
            life = false;
        }

        if (health < jellyTempHealth && health > 0)
        {
            animator.Play("NJellyHurt");
            jellyTempHealth = health;
        }
    }

    public void Init()
    {
        animator = GetComponent<Animator>();
        animator.Play("StrongJellyNormal");
        jellyTempHealth = health;
        life = true;
    }

    IEnumerator DeadLoop()
    {
        Debug.Log("DeadLoop");
        yield return DeadFrame();
        yield return new WaitForSeconds(0.201f);
        yield return Death();
        yield break;
    }

    IEnumerator DeadFrame() // 죽는 애니메이션 종료 후 비활성화
    {
        Debug.Log("DeadFram");
        animator.Play("NJellyDead");
        yield break;
    }

    IEnumerator Death()
    {
        Debug.Log("Death");
        gameObject.SetActive(false);
        GameData.jellyNum--;
        isMoving = false;
        for (int i = 0; i < 5; i++)
        {
            JellyStatus sJelly = stage.gJelly[i].GetComponent<JellyStatus>();
            if (sJelly.jellyCount > gameObject.GetComponent<JellyStatus>().jellyCount)
            {
                sJelly.jellyCount--;
                if (stage.gJelly[i].activeInHierarchy)
                    sJelly.MoveJelly();
            }
        }
        jellyCount = 10;
        yield break;
    }

    public void SetKind(int temp)
    {
        switch (temp)
        {
            case 0:
                NormalJelly normalJelly = GetComponent<NormalJelly>();
                normalJelly.SetStat();
                break;
            case 1:
                StrongJelly strongJelly = GetComponent<StrongJelly>();
                strongJelly.SetStat();
                break;
            case 2:
                BigJelly bigJelly = GetComponent<BigJelly>();
                bigJelly.SetStat();
                break;
        }
    }

    public void MoveJelly() //블럭 움직임
    {
        StartCoroutine(Move()); //Move() 코루틴 실행
    }

    private float _speed = 1.0f; //속도 지정
    public float speed
    {
        get
        {
            return _speed * Time.deltaTime; //이동거리 반환
        }
        private set
        {
            _speed = value; //이동속도 변경
        }
    }

    bool _isMoving; // 블럭이 생성된 후 움직임을 체크
    public bool isMoving
    {
        get { return _isMoving; } //움직임 상태 반환
        private set { _isMoving = value; } //움직임 상태 변경
    }

    IEnumerator Move()
    {
        if (!isMoving) //움직이고 있지 않으면
        {
            isMoving = true; //움직임
            while (isMoving) //움직이는 동안
            {
                if (transform.position.x < GameData.jellyMax.x * 1.2f) //최대 x좌표에 도달했을 경우
                {
                    isMoving = false; //더 이상 움직이지 않음
                    if (transform.position.x < GameData.jellyMax.x)
                    {
                        StartCoroutine(Attack());
                    }
                }
                transform.Translate((Vector2.left * speed).normalized / 40.0f); //블럭 이동
                yield return null; //Update문 수행 완료시까지 대기
            }
        }
        else { yield return null; }
    }

    IEnumerator Attack()
    {
        while (gameObject.activeInHierarchy)
        {
            animator.Play("NJellyAttack");
            float tempHealth = catstatus.health;
            catstatus.health -= damage * 2 - catstatus.defend * 1.5f;
            if (catstatus.health > tempHealth)
                catstatus.health = tempHealth;
            yield return new WaitForSeconds(3.0f);
        }
        yield break;
    }
}
