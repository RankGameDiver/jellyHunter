﻿using UnityEngine;

public static class GameData
{
    public static int blockCount = 0;   // 현재 나와있는 블럭의 개수

    public static bool checkTouchblock = false; // 다른 블럭을 터치했을때 true(블럭이 사라졌을때 빈자리를 다른 블럭이 채우기 위함)

    public static GameObject touchBlock = null; // 터치되는 블럭

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

    public static int StageNum = 0;

    /////////////세이브할 데이터////////////////

    public static int Money = 0;

    public static int StageT = 1;
    public static int Stage1 = 0;
    public static int Stage2 = 0;
    public static int Stage3 = 0;
    public static int ExStage = 0;

    public static int PlayingCount = 5;
    public static int sTime;

    // 업그레이드 상점
    public static int attackUp = 0;
    public static int defendUp = 0;
    public static int healthUp = 0;

    /////////////아이템 적용////////////////////

    public static bool attackItem;
    public static bool defendItem;
    public static bool healItem;
    public static bool moneyItem;
    public static bool healthItem;

    ///////////////////////////////////////////

}