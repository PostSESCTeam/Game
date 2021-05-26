public class DuelScale : Scale
{
    public const int ScaleSize = 10;

    public new void Start()
    {
        base.Start();
        Init(ScaleSize);
    }
}
