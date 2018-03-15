

public class Data
{
    public int money;
    public int stageT;
    public int stage1;
    public int stage2;
    public int stage3;
    public int exStage;

    public int playingCount;
    public int lifeHour;
    public int lifeMin;
    public int lifeSec;

    public Data(int _money = 0, int _stageT = 0, int _stage1 = 0, int _stage2 = 0, int _stage3 = 0, 
        int _exStage = 0, int _playingCount = 0, int _lifeHour = 0, int _lifeMin = 0, int _lifeSec = 0)
    {
        money = _money;
        stageT = _stageT;
        stage1 = _stage1;
        stage2 = _stage2;
        stage3 = _stage3;
        exStage = _exStage;
        playingCount = _playingCount;
        lifeHour = _lifeHour;
        lifeMin = _lifeMin;
        lifeSec = _lifeSec;
    }
}
