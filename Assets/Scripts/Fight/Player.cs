using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 position;

    void Start()
    {
        position = new Vector2();
    }

    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            position += new Vector2(Input.GetAxis("Horizontal") > 0 ? 1 : -1, 0);
        } 
        else if (Input.GetButtonDown("Vertical"))
        {
            position += new Vector2(0, Input.GetAxis("Vertical") > 0 ? 1 : -1);
        } else if (Input.GetKeyDown("Fire"))
        {
            // TODO: ye
        }
    }
}
