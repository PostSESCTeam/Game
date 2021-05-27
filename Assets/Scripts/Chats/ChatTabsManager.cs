using UnityEngine;
using UnityEngine.UI;

public class ChatTabsManager : MonoBehaviour
{
    private bool IsChatOpen = false;
    private Transform message, partnerMessage, button;
    private GameObject contacts, chatWindow, chatsContent, buttons, icons;
    private string partner;
    private Chat chat;
    private Dialog dialog;
    private bool isLiked;

    private void Start()
    {
        GameObject.Find("BackToChatsList").GetComponent<Button>().onClick.AddListener(()
            => FindObjectOfType<ChatTabsManager>().CloseChat());

        message = Resources.Load<Transform>("Message");
        partnerMessage = Resources.Load<Transform>("PartnerMessage");
        button = Resources.Load<Transform>("Button");
        contacts = GameObject.Find("Contacts");
        chatWindow = GameObject.Find("ChatWindow");
        chatsContent = GameObject.Find("ChatsContent");
        buttons = GameObject.Find("Buttons");
        icons = GameObject.Find("Icons");

        chatWindow.SetActive(false);
    }

    public void OpenChat(string partner)
    {
        contacts.SetActive(false);
        chatWindow.SetActive(true);
        icons.SetActive(false);
        IsChatOpen = true;
        isLiked = Main.IsLiked[partner];

        GameObject.Find("Name").GetComponent<Text>().text = partner;

        this.partner = partner;
        chat = Main.Chats[partner];
        dialog = Main.Dialogs[partner];

        foreach (var i in Main.Chats[partner].Messages)
        {
            var messageTr = i.Author == partner ? partnerMessage : message;
            var a = Instantiate(messageTr, chatsContent.transform);
            a.GetComponentInChildren<Text>().text = i.Sentence;
        }

        var buttonsTexts = dialog.GetPlayersPhrases(isLiked);

        if (buttonsTexts.Length > 0)
            foreach (var i in buttonsTexts)
            {
                var a = Instantiate(button, buttons.transform);
                a.GetComponentInChildren<Text>().text = i;
                a.GetComponent<Button>().onClick.AddListener(() => SendMessage(i));
            }

        dialog.phraseIndex++;
    }

    public void CloseChat()
    {
        foreach (Transform i in chatsContent.transform)
            Destroy(i.gameObject);

        contacts.SetActive(true);
        chatWindow.SetActive(false);
        icons.SetActive(true);
        IsChatOpen = false;
    }

    private void SendMessage(string messageStr)
    {
        chat.SendMessage("Player", messageStr);
        var a = Instantiate(message, chatsContent.transform);
        a.GetComponentInChildren<Text>().text = messageStr;

        foreach (Transform i in buttons.transform)
            Destroy(i.gameObject);

        var (c, d) = dialog.GetNextPhrases(isLiked);

        if (c == null)
        {
            if (Random.Range(0f, 1f) <= Main.FightProbabs[partner])
            {
                Main.IsCallingOpen = true;
                Main.IsSwipesFrozen = true;
            }

            Main.FightProbabs.Remove(partner);
            return;
        }

        var partMes = c.GetRandom();
        chat.SendMessage(partner, partMes);
        a = Instantiate(partnerMessage, chatsContent.transform);
        a.GetComponentInChildren<Text>().text = partMes;

        foreach (var i in d)
        {
            a = Instantiate(button, buttons.transform);
            a.GetComponentInChildren<Text>().text = i;
            a.GetComponent<Button>().onClick.AddListener(() => SendMessage(i));
        }
    }
}