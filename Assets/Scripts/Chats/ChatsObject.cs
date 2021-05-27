using UnityEngine;
using UnityEngine.UI;

public class ChatsObject : MonoBehaviour
{
    private void Start()
    {
        Main.ContactsContent = GameObject.Find("ContactsContent");
        Main.CTM = FindObjectOfType<ChatTabsManager>();
    }

    private void Update()
    {
        foreach (Transform i in Main.ContactsContent.transform)
        {
            var chat = Main.Chats[i.Find("Name").GetComponent<Text>().text];
            i.Find("Message").GetComponent<Text>().text = chat.LastMessage.Sentence;
        }
    }
}
