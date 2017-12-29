﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyStatus : MonoBehaviour
{
    [SerializeField]
    private float health; // 체력
    [SerializeField]
    private float damage; // 공격력
    [SerializeField]
    private float defend; // 방어력

    public CatStatus catstatus;
    public Stage stage;
    private Animator animator;
    private Animator effect;

    private float jellyTempHealth;
    private bool life;

    public int jellyCount; // 현재 젤리의 순서
    public int jellyKind; // 젤리 종류

    public static int JKind;
    public static float JHealth;
    public GameObject effectObj;

    void Start()
    {
        effect = effectObj.GetComponent<Animator>();
    }

    void Update()
    {
        JKind = jellyKind;
        JHealth = health;
        if (health <= 0 && life == true) // 젤리맨이 죽었을 때 실행
        {
            life = false;
            StartCoroutine(DeadLoop());
        }
    }

    public void Init()
    {
        animator = GetComponent<Animator>();
        jellyTempHealth = health;
        life = true;
        gameObject.SetActive(true);
    }

    IEnumerator DeadLoop()
    {
        yield return DeadFrame();
        yield return new WaitForSeconds(1.0f);
        yield return Death();
        yield break;
    }

    IEnumerator DeadFrame() // 죽는 애니메이션 종료 후 비활성화
    {
        switch (jellyKind)
        {
            case 0:
                animator.Play("NJellyDead");
                effect.Play("NJellyDeadEft");
                break;
            case 1:
                animator.Play("StrongJellyDead");
                effect.Play("SJellyDeadEft");
                break;
            case 2:
                animator.Play("BJellyDead");
                effect.Play("BJellyDeadEft");
                break;
        }
        yield break;
    }

    IEnumerator Death()
    {
        ScoreManager.PlusDefeatScore(jellyKind + 1);
        if (jellyKind == 2)
        {
            transform.position = new Vector2(transform.position.x, -0.63f);
            animator.Play("BJellyDeadRun");
            for (int i = 0; i < 50; i++)
            {
                transform.Translate((Vector2.right * speed).normalized / 10.0f);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(5.0f);
            effect.Play("temp");
        }
        else
            effect.Play("temp");           
        gameObject.SetActive(false);
        GameData.jellyNum--;
        isMoving = false;
        for (int i = 0; i < 5; i++)
        {
            JellyStatus sJelly = stage.gJelly[i].GetComponent<JellyStatus>();
            if (sJelly.jellyCount > gameObject.GetComponent<JellyStatus>().jellyCount)  sJelly.jellyCount--;
        }
        jellyCount = 10;
        yield break;
    }

    public void SetJelly()
    {
        switch (jellyKind)
        {
            case 0:
                NormalJelly normalJelly = GetComponent<NormalJelly>();
                normalJelly.SetStatus();
                break;
            case 1:
                StrongJelly strongJelly = GetComponent<StrongJelly>();
                strongJelly.SetStatus();
                break;
            case 2:
                BigJelly bigJelly = GetComponent<BigJelly>();
                bigJelly.SetStatus();
                break;
        }
        Init();
        MoveJelly();
    }

    public void MoveJelly() // 젤리 움직임
    {
        switch (jellyKind)
        {
            case 0:
                animator.Play("NJellyNormal");
                gameObject.transform.position = new Vector2(7.5f, -1.9f);
                break;
            case 1:
                animator.Play("StrongJellyNormal");
                gameObject.transform.position = new Vector2(7.5f, -1.9f);
                break;
            case 2:
                animator.Play("BJellyNormal");
                gameObject.transform.position = new Vector2(9.13f, -0.71f);
                break;
        }   
        StartCoroutine(Move()); //Move() 코루틴 실행
    }

    private float _speed = 1.0f; //속도 지정
    public float speed
    {
        get { return _speed * Time.deltaTime; } //이동거리 반환
        private set { _speed = value; } //이동속도 변경
    }

    bool _isMoving; // 블럭이 생성된 후 움직임을 체크
    public bool isMoving
    {
        get { return _isMoving; } //움직임 상태 반환
        private set { _isMoving = value; } //움직임 상태 변경
    }

    IEnumerator Move()
    {
        if (!isMoving && life) //움직이고 있지 않으면
        {
            isMoving = true; //움직임
            while (isMoving) //움직이는 동안
            {
                if (transform.position.x < GameData.jellyMax.x * 1.2f) //최대 x좌표에 도달했을 경우
                {
                    isMoving = false; //더 이상 움직이지 않음
                    StartCoroutine(Attack());
                }
                else if (transform.position.x < 3.84f && jellyKind == 2)
                {
                    isMoving = false;
                    StartCoroutine(Attack());
                }
                transform.Translate((Vector2.left * speed).normalized / 40.0f); //블럭 이동
                yield return null; //Update문 수행 완료시까지 대기
            }
        }
        else { yield return null; }
    }

    IEnumerator Attack()
    {
        while (life)
        {
            switch (jellyKind)
            {
                case 0:
                    animator.Play("NJellyAttack");
                    effect.Play("NJellyAttackEft");
                    break;
                case 1:
                    animator.Play("StrongJellyAttack");
                    effect.Play("SJellyAttackEft");
                    yield return new WaitForSeconds(0.7f); // 젤리 공격 애니메이션과 고양이 피격 타이밍 조절
                    break;
                case 2:
                    animator.Play("BJellyAttack");
                    effect.Play("BJellyAttackEft");
                    break;
            }
            catstatus.Attacked(damage);
            yield return new WaitForSeconds(3.0f);
        }
        yield break;
    }

    public void SetStatus(int hp, int dmg, int def)
    {
        health = hp;
        damage = dmg;
        defend = def;
    }

    public void Attacked(float m_damage)
    {
        jellyTempHealth = health;
        health -= m_damage * (1 - defend / 100);
        if (health > jellyTempHealth) health = jellyTempHealth;
        switch (jellyKind)
        {
            case 0:
                animator.Play("NJellyHurt");
                effect.Play("NJellyHurtEft");
                break;
            case 1:
                animator.Play("StrongJellyHurt");
                effect.Play("SJellyHurtEft");
                break;
            case 2:
                animator.Play("BJellyHurt");
                effect.Play("BJellyHurt2Eft");
                break;
        }
    }
}
