using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    private static int scaleSize = 20;
    public int scaleValue = 10;

    // временно для отладки
    void Awake()
    {
        var form = Form.GenerateForm();
        Debug.Log(form.Name);
        Debug.Log(form.Age);
        Debug.Log(form.Sex);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dt = 1;
        if (true) scaleValue += dt;

    }
}
