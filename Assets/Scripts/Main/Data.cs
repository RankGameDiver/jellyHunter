public class Data
{
    protected int money;
    protected int[] stageKind;

    protected int playingCount;
    protected int sTime;
    protected int[] statUpKind;

    public Data(int _money = 0, int _stageT = 0, int _stage1 = 0, int _stage2 = 0, int _stage3 = 0,
        int _exStage = 0, int _playingCount = 0, int _sTime = 0, int _attackUp = 0, int _defendUp = 0, int _healthUp = 0)
    {
        money = _money;
        playingCount = _playingCount;
        sTime = _sTime;

        stageKind = new int[5];
        stageKind[0] = _stageT;
        stageKind[1] = _stage1;
        stageKind[2] = _stage2;
        stageKind[3] = _stage3;
        stageKind[4] = _exStage;

        statUpKind = new int[3];
        statUpKind[0] = _attackUp;
        statUpKind[1] = _defendUp;
        statUpKind[2] = _healthUp;
    }

    public void SetData(int _money = 0, int _stageT = 0, int _stage1 = 0, int _stage2 = 0, int _stage3 = 0,
        int _exStage = 0, int _playingCount = 0, int _sTime = 0, int _attackUp = 0, int _defendUp = 0, int _healthUp = 0)
    {
        money = _money;
        playingCount = _playingCount;
        sTime = _sTime;

        stageKind[0] = _stageT;
        stageKind[1] = _stage1;
        stageKind[2] = _stage2;
        stageKind[3] = _stage3;
        stageKind[4] = _exStage;

        statUpKind[0] = _attackUp;
        statUpKind[1] = _defendUp;
        statUpKind[2] = _healthUp;
    }

public void Clear()
    {

    }
}

/*
 public class Data
{
    protected int money;
    protected int[] stageKind;

    protected int playingCount;
    protected int sTime;
    protected int[] statUpKind;

    public Data()
    {
        money = 0;
        playingCount = 5;
        sTime = 0;

        stageKind = new int[5];
        stageKind[0] = 0;
        stageKind[1] = 0;
        stageKind[2] = 0;
        stageKind[3] = 0;
        stageKind[4] = 0;

        statUpKind = new int[3];
        statUpKind[0] = 0;
        statUpKind[1] = 0;
        statUpKind[2] = 0;
    }

    public void SetData(int _money = 0, int _stageT = 0, int _stage1 = 0, int _stage2 = 0, int _stage3 = 0,
        int _exStage = 0, int _playingCount = 0, int _sTime = 0, int _attackUp = 0, int _defendUp = 0, int _healthUp = 0)
    {
        money = _money;
        playingCount = _playingCount;
        sTime = _sTime;

        stageKind[0] = _stageT;
        stageKind[1] = _stage1;
        stageKind[2] = _stage2;
        stageKind[3] = _stage3;
        stageKind[4] = _exStage;

        statUpKind[0] = _attackUp;
        statUpKind[1] = _defendUp;
        statUpKind[2] = _healthUp;
    }

public void Clear()
    {

    }
}

     */
