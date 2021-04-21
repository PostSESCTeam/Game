using System;
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
        // do we need it?
        if (Main.IsFormsOpened && Input.GetButtonDown("Horizontal"))
            ChangeFormCard(Input.GetAxis("Horizontal") > 0);
    }

    public void ChangeFormCard(bool isLiked)
    {
        if (isLiked)
            Main.AddLiked(form.CurForm);
        else
            Main.AddDisliked(form.CurForm);
        love += isLiked ? 1 : -1;
        form.ChangeFormCard(isLiked: isLiked);

        if (love <= 0 || love > Scale.ScaleSize)
        {
            Debug.Log("YOU DIED!");
            Destroy(this);
        }
    }

    void UpdateAfterDuel()
    {
        var win = true;
        var d = Math.Sign(Scale.BalancedValue - love);
        scale.UpdateScale(win ? d : -d);
    }
}