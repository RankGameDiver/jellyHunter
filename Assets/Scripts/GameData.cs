using UnityEngine;

public static class GameData
{
    public static float maxXPos = 7.8f;
    public static float maxYPos = -3.0f;
    public static int blockCount = 0;   // 현재 나와있는 블럭의 개수


    // 블럭 생성 좌표
    const float spawnXpos = -8.0f;      //컴파일 타임 값이 정해짐
    const float spawnYpos = -3.0f;   //런타임에 값이 정해짐

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
}