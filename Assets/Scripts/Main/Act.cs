using UnityEngine;
using UnityEngine.SceneManagement;

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
            StartDuel();
    }

    void StartDuel()
    {
        Debug.Log(love);
        SceneManager.LoadSceneAsync("Duel", LoadSceneMode.Additive);
        foreach (var i in SceneManager.GetActiveScene().GetRootGameObjects())
            i.SetActive(false);
    }

    void UpdateAfterDuel(bool isWin)
    {
        love = isWin 
            ? Scale.BalancedValue 
            : Scale.BalancedValue + (int) ((Scale.BalancedValue - love) * 1.5);
        scale.UpdateScale(love);
    }
}