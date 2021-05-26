public class DuelScale : Scale
{
    public const int ScaleSize = 3;

    public new void Start()
    {
        base.Start();
        Init(ScaleSize, 1);
    }
}
