using UnityEngine;

public static class GameData
{
    public static float maxXPos = 8.0f; // 블럭이 이동할 수 있는 최대 x좌표
    public static float maxYPos = -3.0f; // 블럭이 이동할 수 있는 최대 y좌표
    public static int blockCount = 0;   // 현재 나와있는 블럭의 개수

    // 블럭 생성 좌표
    const float spawnXpos = -7.0f;      //컴파일 타임 값이 정해짐
    const float spawnYpos = -3.0f;   //런타임에 값이 정해짐

    public static bool checkTouchblock = false; // 다른 블럭을 터치했을때 true(블럭이 사라졌을때 빈자리를 다른 블럭이 채우기 위함)

    public static GameObject touchBlock; // 터치되는 블럭

    private static Vector2 _spawnPos = new Vector2(spawnXpos, spawnYpos);

    public static Vector2 spawnPos
    {
        get
        {
            return _spawnPos;
        }
        private set
        {
            _spawnPos = value;
        }
    }

    public static Vector2 _maxXPosition = new Vector2(maxXPos, maxYPos);

    public static Vector2 maxXPosition
    {
        get
        {
            return _maxXPosition;
        }
        private set
        {
            _maxXPosition = value;
        }
    }

    public const int blockKinds = 3; // 블럭의 종류
    public const int blockAmount = 7; // 한번에 생성될 수 있는 블럭의 최대 개수

    public static int lastSkillKind = 0; // 전에 쓴 스킬 종류
    public static int skillKind = 0; // 스킬 종류 구분

}