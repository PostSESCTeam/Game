using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    private GameObject chatPlace, frontFormsPlace, backFormsPlace, profilePlace, calling;
    private Animator animator;

    private void Start()
    {
        //TODO: chats (or not to do)
        //chatPlace = GameObject.Find("ChatsPlace");
        frontFormsPlace = GameObject.Find("FrontFormsPlace");
        backFormsPlace = GameObject.Find("BackFormsPlace");
        profilePlace = GameObject.Find("ProfilePlace");
        calling = GameObject.Find("Calling");

        GameObject.Find("Chat").GetComponent<Button>().onClick.AddListener(OpenChat);
        GameObject.Find("Forms").GetComponent<Button>().onClick.AddListener(OpenForms);
        GameObject.Find("Profile").GetComponent<Button>().onClick.AddListener(OpenProfile);

        animator = GameObject.Find("SceneChanger").GetComponent<Animator>();
        foreach (var i in calling.GetComponentsInChildren<Button>())
            i.onClick.AddListener(() => StartCoroutine(Main.StartDuel(animator,
                                                                      FindObjectOfType<Act>().BehaviourName)));
    }

    private void Update()
    {
        // chatPlace.SetActive(Main.IsChatOpened);
        frontFormsPlace.SetActive(Main.IsFormsOpened);
        backFormsPlace.SetActive(Main.IsFormsOpened);
        profilePlace.SetActive(Main.IsProfileOpened);
        calling.SetActive(Main.IsCallingOpen);
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
        Debug.Log("ye i've opened chats");
        //CloseEverything();
        //Main.IsChatOpened = true;
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
