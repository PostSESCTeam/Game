using System;
using UnityEngine;

public class Act : MonoBehaviour
{
    private int love;
    private Scale scale;
    private Form form;

    private void Start() 
    {
        love = Scale.BalancedValue;
        scale = FindObjectOfType<Scale>();
        form = FindObjectOfType<Form>();
    }

    private void Update()
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
        scale.UpdateScale(love);

        if (love <= 0 || love >= Scale.ScaleSize)
        {
            Debug.Log("YOU DIED!");
            Destroy(FindObjectOfType<Swipes>());
            Destroy(this);
        }
        else
            form.ChangeFormCard(isLiked);
    }

    void UpdateAfterDuel()
    {
        var win = true;
        var d = Math.Sign(Scale.BalancedValue - love);
        scale.UpdateScale(win ? d : -d);
    }
}