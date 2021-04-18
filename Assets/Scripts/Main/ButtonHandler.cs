using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    private GameObject chatPlace;
    private GameObject formsPlace;
    private GameObject profilePlace;

    private void Start()
    {
        GameObject.Find("Chat").GetComponent<Button>().onClick.AddListener(OpenChat);
        GameObject.Find("Forms").GetComponent<Button>().onClick.AddListener(OpenForms);
        GameObject.Find("Profile").GetComponent<Button>().onClick.AddListener(OpenProfile);
        //TODO: chats (or not to do)
        //chatPlace = GameObject.Find("ChatsPlace");
        formsPlace = GameObject.Find("FormsPlace");
        profilePlace = GameObject.Find("ProfilePlace");
        Update();
    }

    private void Update()
    {
        //chatPlace.SetActive(Main.IsChatOpened);
        formsPlace.SetActive(Main.IsFormsOpened);
        profilePlace.SetActive(Main.IsProfileOpened);
    }

    // We have no chats, so this part should not work
    public void OpenChat()
    {
        //Main.IsChatOpened = true;
        //Main.IsFormsOpened = false;
        //Main.IsProfileOpened = false;
    }

    public void OpenForms()
    {
        Main.IsChatOpened = false;
        Main.IsFormsOpened = true;
        Main.IsProfileOpened = false;
    }

    public void OpenProfile()
    {
        Main.IsChatOpened = false;
        Main.IsFormsOpened = false;
        Main.IsProfileOpened = true;
    }
}
