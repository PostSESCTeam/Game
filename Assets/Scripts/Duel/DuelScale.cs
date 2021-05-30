public class DuelScale : Scale
{
    public const int ScaleSize = 7;

    public new void Start()
    {
        base.Start();
        Init(ScaleSize, 1);
    }
}
