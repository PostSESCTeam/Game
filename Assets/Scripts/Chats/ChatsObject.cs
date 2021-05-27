using UnityEngine;
using UnityEngine.UI;

public class ChatsObject : MonoBehaviour
{
    private GameObject contactsContent;

    private void Start() => contactsContent = GameObject.Find("ContactsContent");

    private void Update()
    {
        foreach (Transform i in contactsContent.transform)
        {
            var chat = Main.Chats[i.Find("Name").GetComponent<Text>().text];
            i.Find("Message").GetComponent<Text>().text = chat.LastMessage.Sentence;
        }
    }
}
