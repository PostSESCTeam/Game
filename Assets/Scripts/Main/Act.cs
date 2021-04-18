using System;
using System.Collections.Generic;
using UnityEngine;

public class Act : MonoBehaviour
{
    private int love;
    private Scale scale;
    private Form form;

    void Start() 
    {
        love = Scale.BalancedValue;
        scale = FindObjectOfType<Scale>();
        form = FindObjectOfType<Form>();
    }

    void Update()
    {
        if (Main.IsFormsOpened && Input.GetButtonDown("Horizontal"))
        {
            var liked = Input.GetAxis("Horizontal") > 0;
            if (liked)
                Main.AddLiked(form.CurForm);
            else
                Main.AddDisliked(form.CurForm);
            love += liked ? 1 : -1;
            form.ChangeFormCard();

            if (love <= 0 || love > Scale.ScaleSize)
            {
                Debug.Log("YOU DIED!");
                Destroy(this);
            }
        }  
    }

    void UpdateAfterDuel()
    {
        var win = true;
        var d = Math.Sign(Scale.BalancedValue - love);
        scale.UpdateScale(win ? d : -d);
    }
}