using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyStatus : MonoBehaviour
{
    public int health; // 체력
    public int damage; // 공격력
    public int defend; // 방어력

    void Start()
    {

    }

    void Update()
    {
        if (health <= 0) // 젤리맨이 죽었을 때 실행
        {
            gameObject.SetActive(false);
        }
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

    IEnumerator Move() // 생성할때
    {
        if (isMoving == false) //움직이고 있지 않으면
        {
            isMoving = true; //움직임
            while (isMoving) //움직이는 동안
            {
                if (transform.position.x < GameData.jellyMax.x) //최대 x좌표에 도달했을 경우
                {
                    isMoving = false; //더 이상 움직이지 않음
                }
                transform.Translate((Vector2.left * speed).normalized / 10.0f); //블럭 이동
                yield return null; //Update문 수행 완료시까지 대기
            }
        }
    }
}
