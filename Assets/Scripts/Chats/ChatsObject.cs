using System.Collections.Generic;
using UnityEngine;

public class ChatsObject : MonoBehaviour
{
    private GameObject chatsContent;
    private Transform contact;
    private List<Chat> chats;

    private void Start()
    {
        chatsContent = GameObject.Find("ChatsContent");
        contact = Resources.Load<Transform>("Contact");
    }

    private void Update()
    {
        while (chatsContent.transform.childCount < 5)
            Instantiate(contact, chatsContent.transform);
    }
}
