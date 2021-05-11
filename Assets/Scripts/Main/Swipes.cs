using UnityEngine;

public class Swipes : MonoBehaviour
{
    private Transform Form;
    private float x1, x2;
    private Rotation rotation;
    private const int MinSwipeLenX = 20;
    private const int RotationSpeed = 100;
    private const double MaxAngle = 0.5;
    private readonly Vector3 RotationPoint = new Vector3(0f, -6f);
    private readonly Vector3 RotationAxis = new Vector3(0f, 0f, 1f);

    void Start()
    {
        Form = GameObject.Find("FrontFormsPlace").transform;
        rotation = Rotation.None;
    }

    void Update()
    {
        if (!Main.IsFormsOpened || Main.IsSwipesFrozen)
            return;

        if (Input.GetMouseButtonDown(0))
            x1 = Input.mousePosition.x;

        if (Input.GetMouseButtonUp(0))
        {
            x2 = Input.mousePosition.x;
            if (Mathf.Abs(x1 - x2) < MinSwipeLenX)
                return;
            if (x1 < x2)
                rotation = Rotation.Right;
            else 
                rotation = Rotation.Left;
        }

        Form.RotateAround(RotationPoint, RotationAxis, (int) rotation * -RotationSpeed * Time.deltaTime);

        if (Mathf.Abs(Form.transform.rotation.z) >= MaxAngle)
        {
            FindObjectOfType<Act>().ChangeFormCard(rotation > 0);
            rotation = Rotation.None;
            Form.RotateAround(RotationPoint, RotationAxis, -Form.transform.eulerAngles.z);
        }
    }
}

public enum Rotation
{
    None,
    Left = -1,
    Right = 1
}
