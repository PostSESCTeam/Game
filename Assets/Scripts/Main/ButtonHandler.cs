using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    private GameObject chatPlace, frontFormsPlace, backFormsPlace, profilePlace, calling;
    private Animator animator;

    private void Start()
    {
        chatPlace = GameObject.Find("ChatPlace");
        frontFormsPlace = GameObject.Find("FrontFormsPlace");
        backFormsPlace = GameObject.Find("BackFormsPlace");
        profilePlace = GameObject.Find("ProfilePlace");
        calling = GameObject.Find("Calling");

        animator = GameObject.Find("SceneChanger").GetComponent<Animator>();
        foreach (var i in calling.GetComponentsInChildren<Button>())
            i.onClick.AddListener(() => StartCoroutine(Main.StartDuel(animator, FindObjectOfType<Act>().BehaviourName)));

        var chat = GameObject.Find("ChatIcon");
        Main.ChatsBtnImg = chat.GetComponent<Image>(); 
        chat.GetComponent<Button>().onClick.AddListener(OpenChat);
        GameObject.Find("FormsIcon").GetComponent<Button>().onClick.AddListener(OpenForms);
        GameObject.Find("ProfileIcon").GetComponent<Button>().onClick.AddListener(OpenProfile);
    }

    private void Update()
    {
        chatPlace.SetActive(Main.IsChatOpened);
        frontFormsPlace.SetActive(Main.IsFormsOpened);
        backFormsPlace.SetActive(Main.IsFormsOpened);
        profilePlace.SetActive(Main.IsProfileOpened);
        calling.SetActive(Main.IsCallingOpen);

        if (Main.IsFirstDuel && Main.IsCallingOpen)
            Main.DuelTutorial.SetActive(true);
    }

    private void CloseEverything()
    {
        Main.IsChatOpened = false;
        Main.IsFormsOpened = false;
        Main.IsProfileOpened = false;
        Main.IsCallingOpen = false;
    }

    private void OpenChat()
    {
        CloseEverything();
        Main.IsChatOpened = true;

        Main.ChatsBtnImg.sprite = Main.RegularChats;
    }

    private void OpenForms()
    {
        CloseEverything();
        Main.IsFormsOpened = true;
    }

    public void OpenProfile()
    {
        CloseEverything();
        Main.IsProfileOpened = true;
    }
}
