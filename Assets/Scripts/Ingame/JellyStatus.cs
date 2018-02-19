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
    private Animator animator { get { return GetComponent<Animator>(); } }
    private Animator effect { get { return effectObj.GetComponent<Animator>(); } }
    private SoundM soundM { get { return GetComponent<SoundM>(); } }

    private float jellyTempHealth;
    private bool life;

    public int jellyCount; // 현재 젤리의 순서
    public int jellyKind; // 젤리 종류

    public RectTransform pos { get{return GetComponent<RectTransform>(); } }
    public GameObject effectObj;

    public GameObject hpBar;

    private float _speed = 1.0f; //속도 지정
    public float speed
    {
        get { return _speed * Time.deltaTime; } //이동거리 반환
        private set { _speed = value; } //이동속도 변경
    }

    public bool isMoving; // 젤리가 생성된 후 움직임을 체크

    void Update()
    {
        if (health <= 0 && life == true) // 젤리맨이 죽었을 때 실행
        {
            life = false;
            StartCoroutine(DeadLoop());
        }
    }

    public void Init()
    {
        if (!hpBar.activeInHierarchy)
            hpBar.SetActive(true);
        jellyTempHealth = health;
        life = true;
        gameObject.SetActive(true);
    }

    public void SetJelly()
    {
        switch (jellyKind)
        {
            case 0:
                SetStatus(50, 5, 0); // Normal Jelly
                break;
            case 1:
                SetStatus(120, 10, 5); // Strong Jelly
                break;
            case 2:
                SetStatus(250, 20, 10); // Big Jelly
                break;
        }
        Init();
        MoveJelly();
    }

    public void SetStatus(int hp, int dmg, int def)
    {
        health = hp;
        damage = dmg;
        defend = def;
    }

    public void MoveJelly() // 젤리 움직임
    {
        switch (jellyKind)
        {
            case 0:
                animator.Play("NJellyNormal");
                pos.anchoredPosition = new Vector2(170, -219); 
                break;
            case 1:
                animator.Play("StrongJellyNormal");
                pos.anchoredPosition = new Vector2(170, -171); 
                break;
            case 2:
                animator.Play("BJellyNormal");
                pos.anchoredPosition = new Vector2(380, -28); 
                break;
        }
        StartCoroutine(Move()); //Move() 코루틴 실행
    }

    IEnumerator Move()
    {
        if (!isMoving && life) //움직이고 있지 않으면
        {
            isMoving = true; //움직임
            while (isMoving) //움직이는 동안
            {
                if (pos.anchoredPosition.x < GameData.jellyMax.x) //최대 x좌표에 도달했을 경우
                {
                    isMoving = false; //더 이상 움직이지 않음
                    StartCoroutine(Attack());
                }
                else if (pos.anchoredPosition.x < -452.0f && jellyKind == 2)
                {
                    isMoving = false;
                    StartCoroutine(Attack());
                }
                pos.Translate((Vector2.left * speed).normalized / 40.0f);
                yield return null; //Update문 수행 완료시까지 대기
            }
            soundM.PlaySound(jellyKind * 2);
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
                    catstatus.Attacked(damage);
                    break;
                case 1:
                    animator.Play("StrongJellyAttack");
                    effect.Play("SJellyAttackEft");
                    yield return new WaitForSeconds(0.7f); // 젤리 공격 애니메이션과 고양이 피격 타이밍 조절
                    catstatus.Attacked(damage);
                    break;
                case 2:
                    animator.Play("BJellyAttack");
                    effect.Play("BJellyAttackEft");
                    catstatus.Attacked(damage);                   
                    break;
            }
            soundM.PlaySound(jellyKind * 2 + 1);
            yield return new WaitForSeconds(3.0f);
        }
        yield break;
    }

    public void Attacked(float m_damage)
    {
        jellyTempHealth = health;
        health -= m_damage * (1 - defend / 100);
        if (health > jellyTempHealth) health = jellyTempHealth;
        if (life)
        {
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
            pos.anchoredPosition = new Vector2(pos.anchoredPosition.x, -219);
            animator.Play("BJellyDeadRun");
            hpBar.SetActive(false);
            for (int i = 0; i < 50; i++)
            {
                pos.Translate((Vector2.right * speed).normalized / 10.0f);
                yield return new WaitForSeconds(0.1f);
            }
            effect.Play("temp");
        }
        else
        {
            hpBar.SetActive(false);
            effect.Play("temp");
        }
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

    public float GetHealth()
    {
        return health;
    }
}
