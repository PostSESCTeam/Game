using UnityEngine;
using UnityEngine.UI;

public class ChatTabsManager : MonoBehaviour
{
    private bool isContactsOpen = true;
    private Transform message;
    private GameObject contacts, chat, chatsContent;

    private void Start()
    {
        message = Resources.Load<Transform>("Message");
        contacts = GameObject.Find("Contacts");
        chat = GameObject.Find("Chat");
        chatsContent = GameObject.Find("ChatsContent");

        chat.SetActive(false);
    }

    //private void Update()
    //{
    //    contacts.SetActive(isContactsOpen);
    //    chat.SetActive(!isContactsOpen);
    //}

    public void OpenChat(string partner)
    {
        contacts.SetActive(false);
        chat.SetActive(true);

        foreach (var i in Main.Chats[partner].Messages)
        {
            var a = Instantiate(message, chatsContent.transform);
            a.GetComponent<Text>().text = i.Sentence;
        }
    }
}