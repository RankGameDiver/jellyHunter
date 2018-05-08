using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyStatus : MonoBehaviour
{
    public struct Jelly
    {
        public float health; // 체력
        public float damage; // 공격력
        public float defend; // 방어력
        public int jellyCount; // 현재 젤리의 순서
        public int jellyKind; // 젤리 종류
    }

    enum Monster { Normal, Strong, Big, Bomb };

    public Jelly jelly;
    public CatStatus catstatus;
    public Stage stage;
    private Animator animator { get { return gameObject.GetComponent<Animator>(); } }
    private Animator effect { get { return effectObj.GetComponent<Animator>(); } }
    private SoundM soundM { get { return gameObject.GetComponent<SoundM>(); } }
    public SoundM eftSound;

    private bool life;
    public RectTransform pos { get{return gameObject.GetComponent<RectTransform>(); } }
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
        if (jelly.health <= 0 && life == true) // 젤리맨이 죽었을 때 실행
        {
            life = false;
            StartCoroutine(DeadLoop());
        }
    }

    public void Init()
    {
        hpBar.GetComponent<JellyHpBar>().SetHpBar();
        if (!hpBar.activeInHierarchy)
            hpBar.SetActive(true);
        life = true;
        gameObject.SetActive(true);
    }

    public void SetJelly()
    {
        switch (jelly.jellyKind)
        {
            case (int)Monster.Normal:
                SetStatus(50, 5, 0); // Normal Jelly
                break;
            case (int)Monster.Strong:
                SetStatus(120, 10, 5); // Strong Jelly
                break;
            case (int)Monster.Big:
                SetStatus(250, 20, 10); // Big Jelly
                break;
            case (int)Monster.Bomb:
                SetStatus(75, 15, 0);
                break;
        }
        Init();
        MoveJelly();
    }

    public void SetStatus(int hp, int dmg, int def)
    {
        jelly.health = hp;
        jelly.damage = dmg;
        jelly.defend = def;
    }

    public void MoveJelly() // 젤리 움직임
    {
        switch (jelly.jellyKind)
        {
            case (int)Monster.Normal:
                animator.Play("NJellyNormal");
                pos.anchoredPosition = new Vector2(170, -219);
                break;
            case (int)Monster.Strong:
                animator.Play("StrongJellyNormal");
                pos.anchoredPosition = new Vector2(170, -171); 
                break;
            case (int)Monster.Big:
                animator.Play("BJellyNormal");
                pos.anchoredPosition = new Vector2(380, -28); 
                break;
            case (int)Monster.Bomb:
                animator.Play("BombJellyNormal");
                pos.anchoredPosition = new Vector2(170, -170); // 아마 수정 필요
                break;
                
        }
        StartCoroutine(Move()); //Move() 코루틴 실행
    }

    IEnumerator Move()
    {
        if (!isMoving && life) //움직이고 있지 않으면
        {
            isMoving = true; //움직임
            soundM.SetSoundClip(jelly.jellyKind);
            while (isMoving) //움직이는 동안
            {
                if (pos.anchoredPosition.x < GameData.jellyMax.x) //최대 x좌표에 도달했을 경우
                {
                    isMoving = false; //더 이상 움직이지 않음
                    StartCoroutine(Attack());
                }
                else if (pos.anchoredPosition.x < -452.0f && jelly.jellyKind == (int)Monster.Big)
                {
                    isMoving = false;
                    StartCoroutine(Attack());
                }
                else if (pos.anchoredPosition.x < -400.0f && jelly.jellyKind == (int)Monster.Bomb)
                {
                    isMoving = false;
                    StartCoroutine(Attack());
                }
                pos.Translate((Vector2.left * speed).normalized / 40.0f);              
                yield return null; //Update문 수행 완료시까지 대기
            }
        }
        else { yield return null; }
    }

    IEnumerator Attack()
    {
        while (life)
        {
            switch (jelly.jellyKind)
            {
                case (int)Monster.Normal:
                    animator.Play("NJellyAttack");
                    effect.Play("NJellyAttackEft");
                    catstatus.Attacked(jelly.damage);
                    break;
                case (int)Monster.Strong:
                    animator.Play("StrongJellyAttack");
                    effect.Play("SJellyAttackEft");
                    yield return new WaitForSeconds(0.7f); // 젤리 공격 애니메이션과 고양이 피격 타이밍 조절
                    catstatus.Attacked(jelly.damage);
                    break;
                case (int)Monster.Big:
                    animator.Play("BJellyAttack");
                    effect.Play("BJellyAttackEft");
                    catstatus.Attacked(jelly.damage);
                    break;
                case (int)Monster.Bomb:
                    animator.Play("BombJellyAttack");
                    effect.Play("BombJellyAttackEft");
                    yield return new WaitForSeconds(0.7f);
                    catstatus.Attacked(jelly.damage);
                    break;

            }
            eftSound.PlaySound(jelly.jellyKind);
            yield return new WaitForSeconds(3.0f);
        }
        yield break;
    }

    public void Attacked(float m_damage)
    {
        float jellyTempHealth = jelly.health;
        jelly.health -= m_damage * (1 - jelly.defend / 100);
        if (jelly.health > jellyTempHealth) jelly.health = jellyTempHealth;
        if (life)
        {
            switch (jelly.jellyKind)
            {
                case (int)Monster.Normal:
                    animator.Play("NJellyHurt");
                    effect.Play("NJellyHurtEft");
                    break;
                case (int)Monster.Strong:
                    animator.Play("StrongJellyHurt");
                    effect.Play("SJellyHurtEft");
                    break;
                case (int)Monster.Big:
                    animator.Play("BJellyHurt");
                    effect.Play("BJellyHurt2Eft");
                    break;
                case (int)Monster.Bomb:
                    animator.Play("BombJellyHurt");
                    effect.Play("BombJellyHurtEft");
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
        switch (jelly.jellyKind)
        {
            case (int)Monster.Normal:
                animator.Play("NJellyDead");
                effect.Play("NJellyDeadEft");
                break;
            case (int)Monster.Strong:
                animator.Play("StrongJellyDead");
                effect.Play("SJellyDeadEft");
                break;
            case (int)Monster.Big:
                animator.Play("BJellyDead");
                effect.Play("BJellyDeadEft");
                break;
            case (int)Monster.Bomb:
                animator.Play("BombJellyDead");
                effect.Play("NJellyDeadEft");
                break;
        }
        yield break;
    }

    IEnumerator Death()
    {
        ScoreManager.PlusDefeatScore(jelly.jellyKind + 1);
        if (jelly.jellyKind == (int)Monster.Big)
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
            if (sJelly.jelly.jellyCount > gameObject.GetComponent<JellyStatus>().jelly.jellyCount)  sJelly.jelly.jellyCount--;
        }
        jelly.jellyCount = 10;
        yield break;
    }

    public float GetHealth()
    {
        return jelly.health;
    }
}
