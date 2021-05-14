using Random = System.Random;

public static class RandomExtensions 
{
    public static float NextFloat(this Random rnd, float min, float max)
    {
        return (float)rnd.NextDouble() * (max - min) + min;
    }
}
