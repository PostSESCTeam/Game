using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Act : MonoBehaviour
{
    public Animator animator;
    private static int love;
    private static Scale scale;
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
        var fightProb = form.CurForm.FightProbability;

        if (love <= 0 || love >= Scale.ScaleSize)
        {
            Debug.Log("YOU DIED!");
            Destroy(FindObjectOfType<Swipes>());
            Destroy(gameObject);
        }
        else
            form.ChangeFormCard(isLiked);

        var rand = new System.Random();
        if (rand.NextDouble() <= fightProb)
            StartFade();
    }

    public void StartFade()
    {
        animator.SetTrigger("FadeOut");
    }

    public void StartDuel()
    {
        Debug.Log(love);
        SceneManager.LoadSceneAsync("Duel", LoadSceneMode.Additive);
        foreach (var i in SceneManager.GetActiveScene().GetRootGameObjects())
            i.SetActive(false);
    }

    public static void UpdateAfterDuel(bool isWin)
    {
        //love = isWin 
          //  ? Scale.BalancedValue 
            //: Scale.BalancedValue + (int) ((Scale.BalancedValue - love) * 1.5);
            var d = (int) Mathf.Sign(Scale.BalancedValue - love) * 3;
        if (love != Scale.BalancedValue)
        {
            if (isWin && 3 < Mathf.Abs(Scale.BalancedValue - love))
                love += d;
            else if (isWin)
                love = Scale.BalancedValue;
            else
                love -= d;
        } 
        //love = isWin ? love + d : love - d;
        scale.UpdateScale(love);
    }
}