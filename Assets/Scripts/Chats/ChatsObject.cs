using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChatsObject : MonoBehaviour
{
    private ChatTabsManager ctm;
    private GameObject contactsContent;
    private Transform contact;
    public List<Chat> Chats;

    private void Start()
    {
        ctm = FindObjectOfType<ChatTabsManager>();
        contactsContent = GameObject.Find("ContactsContent");
        contact = Resources.Load<Transform>("Contact");
        Chats = new List<Chat>();
    }

    private void Update()
    {
        if (contactsContent.transform.childCount < Main.Chats.Count)
        {
            var chat = Main.Chats.Last().Value;
            var contactItem = Instantiate(contact, contactsContent.transform);
            contactItem.Find("Name").GetComponent<Text>().text = chat.Partner;
            contactItem.Find("Message").GetComponent<Text>().text = chat.LastMessage.Sentence;
            contactItem.GetComponent<Button>().onClick.AddListener(() => ctm.OpenChat(chat.Partner));
        }
    }
}
