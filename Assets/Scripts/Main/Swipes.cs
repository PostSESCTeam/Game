using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipes : MonoBehaviour
{
    public GameObject Form;
    public float x1;
    public float x2;
    public float move = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            x1 = Input.mousePosition.x;
        }
        if (Input.GetMouseButtonUp(0))
        {
            x2 = Input.mousePosition.x;
            if (x1 > x2)
                move = 1f;
            if (x2 > x1)
                move = 2f;
        }

        if (move == 1)
        {
            Form.transform.RotateAround(new Vector3(0f, -6f), new Vector3(0f, 0f, 1f), 100 * Time.deltaTime);
        }
        if (move == 2)
        {
            Form.transform.RotateAround(new Vector3(0f, -6f), new Vector3(0f, 0f, 1f), -100 * Time.deltaTime);
        }
        //Debug.Log(Mathf.Abs(Form.transform.rotation.z));
        if (Mathf.Abs(Form.transform.rotation.z) >= 0.5)
        {
            Debug.Log("should stop");
            move = 0;
        }
    }
}
