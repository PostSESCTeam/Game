using UnityEngine;

public class Act : MonoBehaviour
{
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

        if (Random.Range(0f, 1f) <= fightProb)
            StartCoroutine(Main.StartDuel(GameObject.Find("SceneChanger").GetComponent<Animator>()));
    }

    public static void UpdateAfterDuel(bool isWin)
    {
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
        scale.UpdateScale(love);
    }
}