using UnityEngine;

public static class GameData
{
    const float maxXPos = 4.95f; // 블럭이 이동할 수 있는 최대 x좌표
    const float maxYPos = -3.9f; // 블럭이 이동할 수 있는 최대 y좌표
    public static int blockCount = 0;   // 현재 나와있는 블럭의 개수

    // 블럭 생성 좌표
    const float spawnXpos = -6.0f;
    const float spawnYpos = -4.0f;

    public static bool checkTouchblock = false; // 다른 블럭을 터치했을때 true(블럭이 사라졌을때 빈자리를 다른 블럭이 채우기 위함)

    public static GameObject touchBlock = null; // 터치되는 블럭

    public static Vector2 _spawnPos = new Vector2(spawnXpos, spawnYpos); //스폰 좌표

    public static Vector2 spawnPos
    {
        get{ return _spawnPos; } //스폰 좌표 반환        
        set{ _spawnPos = value; }//스폰 좌표 변경
    }

    private static Vector2 _maxPos = new Vector2(maxXPos, maxYPos); //최대 이동 좌표

    public static Vector2 maxPos
    {
        get { return _maxPos; }//최대 이동 좌표 반환
        set { _maxPos = value; }//최대 이동 좌표 변경
    }

    public const int blockKinds = 3; // 블럭의 종류
    public const int blockAmount = 7; // 한번에 생성될 수 있는 블럭의 최대 개수

    public static int lastSkillKind = 0; // 전에 쓴 스킬 종류
    public static int skillKind = 0; // 스킬 종류 구분

    public static int skillPower = 0; // 스킬 체인 수

    /////////////////////////젤리맨/////////////////////////////

    public static int jellyNum = 0; // 한 스테이지에서 나온 젤리맨 갯수 카운팅

    const float jellyMaxX = -1300.0f;
    const float jellyMaxY = 0.0f;

    private static Vector2 _jellyMax = new Vector2(jellyMaxX, jellyMaxY);
    public static Vector2 jellyMax
    {
        get { return _jellyMax; }
        set { _jellyMax = value; }
    }

    ///////////////////////////////////////////////////////////

    /////////////////////스테이지////////////////////////

    private static int _StageNum = 0;
    public static int StageNum
    {
        get { return _StageNum; }
        set { _StageNum = value; }
    }

    /////////////세이브할 데이터////////////////

    private static int _Money = 0;
    public static int Money
    {
        get { return _Money; }
        set { _Money = value; }
    }

    private static int _StageT = 0;
    public static int StageT
    {
        get{ return _StageT; }
        set{ _StageT = value; }
    }

    private static int _Stage1 = 0;
    public static int Stage1
    {
        get { return _Stage1; }
        set { _Stage1 = value; }
    }

    private static int _Stage2 = 0;
    public static int Stage2
    {
        get { return _Stage2; }
        set { _Stage2 = value; }
    }

    private static int _Stage3 = 0;
    public static int Stage3
    {
        get { return _Stage3; }
        set { _Stage3 = value; }
    }

    private static int playCount;

    /////////////아이템 적용////////////////////

    public static bool attackUp;
    public static bool defendUp;
    public static bool healUp;
    public static bool moneyUp;
    public static bool hpUp;

    ///////////////////////////////////////////

}