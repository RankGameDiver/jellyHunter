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

    void Start()
    {
        animator = GetComponent<Animator>();
        jellyTempHealth = health;
    }

    void Update()
    {
        if (health <= 0) // 젤리맨이 죽었을 때 실행
        {
            StartCoroutine(DeadLoop());
        }

        if (health < jellyTempHealth)
        {
            animator.Play("NJellyHurt");
            jellyTempHealth = health;
        }
    }

    IEnumerator DeadLoop()
    {
        Debug.Log("DeadLoop");
        yield return DeadFrame();
        yield return new WaitForSeconds(0.3f);
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

    public void NomalJelly()
    {
        health = 50;
        damage = 8;
        defend = 5;
    }

    public void StrongJelly()
    {
        health = 120;
        damage = 15;
        defend = 10;
    }

    public void BigJelly()
    {
        health = 300;
        damage = 25;
        defend = 20;
    }

    public void Init(int temp)
    {
        switch (temp)
        {
            case 0:
                NomalJelly();
                break;
            case 1:
                StrongJelly();
                break;
            case 2:
                BigJelly();
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
        if (isMoving == false) //움직이고 있지 않으면
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
