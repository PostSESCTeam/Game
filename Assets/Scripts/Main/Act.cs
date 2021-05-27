using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Act : MonoBehaviour
{
    private int love;
    private MainScale scale;
    private Form form;
    public string BehaviourName;
    public GameObject death;

    private void Start() 
    {
        love = MainScale.BalancedValue;
        scale = FindObjectOfType<MainScale>();
        form = FindObjectOfType<Form>();
        death = GameObject.Find("Death");
        GameObject.Find("OKDeath").GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        death.SetActive(false);
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
        BehaviourName = form.CurForm.CharacterSet;
        var isSpecial = form.CurForm.IsSpecial;

        if (love <= 0 || love >= MainScale.ScaleSize)
        {
            Debug.Log("YOU DIED!");
            death.SetActive(true);
            death.GetComponent<Animator>().SetTrigger("IsDead");
            Main.IsSwipesFrozen = true;
            Destroy(gameObject);
        }
        else
            form.ChangeFormCard(isLiked);

        if (!isSpecial && Random.Range(0f, 1f) <= fightProb)
        {
            Main.IsCallingOpen = true;
            Main.IsSwipesFrozen = true;
        }
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