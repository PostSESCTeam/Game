using System;
using System.Collections.Generic;
using UnityEngine;

public class Act : MonoBehaviour
{
    Scale scale;
    Form form;
    void Start() 
    {
        scale = FindObjectOfType<Scale>();
        form = FindObjectOfType<Form>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            form.ChangeFormCard();
            var liked = Input.GetAxis("Horizontal") > 0;
            if (liked)
                scale.UpdateScale(1);
            else
                scale.UpdateScale(-1);
        }  
    }

    void UpdateAfterDuel()
    {
        var win = true;
        var s = scale.scaleValue;
        var d = Math.Sign(scale.balancedValue - s) * 4;
        scale.UpdateScale(win ? d : -d);
    }
}
