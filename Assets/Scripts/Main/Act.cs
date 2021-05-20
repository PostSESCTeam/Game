using UnityEngine;

public class Act : MonoBehaviour
{
    private int love;
    private MainScale scale;
    private Form form;
    private Animator animator;

    private void Start() 
    {
        love = MainScale.BalancedValue;
        scale = FindObjectOfType<MainScale>();
        form = FindObjectOfType<Form>();
        animator = GameObject.Find("SceneChanger").GetComponent<Animator>();
    }

    public void ChangeFormCard(bool isLiked)
    {
        if (isLiked)
            Main.Like(form.CurForm);
        else
            Main.Dislike(form.CurForm);
            
        love += isLiked ? 1 : -1;
        scale.UpdateScale(love);
        var fightProb = form.CurForm.FightProbability;

        if (love <= 0 || love >= MainScale.ScaleSize)
        {
            Debug.Log("YOU DIED!");
            Main.IsSwipesFrozen = true;
            Destroy(gameObject);
        }
        else
            form.ChangeFormCard(isLiked);

        if (Random.Range(0f, 1f) <= fightProb)
            StartCoroutine(Main.StartDuel(animator));
    }

    public void UpdateAfterDuel(bool isWin)
    {
        Debug.Log(isWin ? "You win!" : "You lose!");
        var d = (int) Mathf.Sign(MainScale.BalancedValue - love) * 3;
        if (love != MainScale.BalancedValue)
        {
            if (isWin && Mathf.Abs(MainScale.BalancedValue - love) > 3)
                love += d;
            else if (isWin)
                love = MainScale.BalancedValue;
            else
                love -= d;
        }
        scale.UpdateScale(love);
    }
}