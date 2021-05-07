public class Rival : DuelObject
{
    private const float fireRate = 1.5f;

    private new void Update()
    {
        base.Update();
        var target = FindObjectOfType<Player>().gameObject.transform.position;
        Rotate(target);
        StartCoroutine(AutoShoot(fireRate));
    }
}
