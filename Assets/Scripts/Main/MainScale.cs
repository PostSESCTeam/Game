public class MainScale : Scale
{
    public const int ScaleSize = 20, BalancedValue = ScaleSize / 2;

    private new void Start()
    {
        base.Start();
        Init(ScaleSize);
    }
}