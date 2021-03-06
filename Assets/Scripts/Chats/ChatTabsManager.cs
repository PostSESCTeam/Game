using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ChatTabsManager : MonoBehaviour
{
    private Transform message, partnerMessage, button;
    private GameObject contacts, chatWindow, chatsContent, buttons, icons;
    private string partner;
    private Chat chat;
    private Dialog dialog;
    private bool isLiked, isChatOpen;

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

    private void Update()
    {
        contacts.SetActive(!isChatOpen);
        chatWindow.SetActive(isChatOpen);
        icons.SetActive(!isChatOpen);
    }

    public void OpenChat(string partner)
    {
        isLiked = Main.IsLiked[partner];
        isChatOpen = true;
        Update();

        GameObject.Find("PartnerName").GetComponent<Text>().text = partner;

        this.partner = partner;
        chat = Main.Chats[partner];
        dialog = Main.Dialogs[partner];

        foreach (var i in Main.Chats[partner].Messages)
        {
            var messageTr = i.Author == partner ? partnerMessage : this.message;
            var message = Instantiate(messageTr, chatsContent.transform);
            message.GetComponentInChildren<Text>().text = i.Sentence;
        }

        if (dialog.IsEnded && chat.Messages.Count % 2 == 0)
            return;

        foreach (var i in dialog.GetPlayersPhrases(isLiked))
        {
            var answerBtn = Instantiate(button, buttons.transform);
            answerBtn.GetComponentInChildren<Text>().text = i;
            answerBtn.GetComponent<Button>().onClick.AddListener(() => SendMessage(i));
        }

        dialog.MoveToNextPhrases();
    }

    public void CloseChat()
    {
        if (!dialog.IsEnded || chat.Messages.Count % 2 == 1)
            dialog.MoveToPrevPhrases();

        foreach (Transform i in chatsContent.transform)
            Destroy(i.gameObject);

        foreach (Transform i in buttons.transform)
            Destroy(i.gameObject);

        isChatOpen = false;
    }

    private new void SendMessage(string messageStr)
    {
        chat.SendMessage("Player", messageStr);
        var message = Instantiate(this.message, chatsContent.transform);
        message.GetComponentInChildren<Text>().text = messageStr;

        foreach (Transform i in buttons.transform)
            Destroy(i.gameObject);

        var (partnerPhrases, playerPhrases) = dialog.GetNextPhrases(isLiked);

        if (partnerPhrases == null)
        {
            if (Random.Range(0f, 1f) <= Main.FightProbabs[partner])
                StartCoroutine(SetupDuel());

            Main.FightProbabs.Remove(partner);
            return;
        }

        dialog.MoveToNextPhrases();
        var partMes = partnerPhrases.GetRandom();
        chat.SendMessage(partner, partMes);
        message = Instantiate(partnerMessage, chatsContent.transform);
        message.GetComponentInChildren<Text>().text = partMes;

        foreach (var i in playerPhrases)
        {
            var answerBtn = Instantiate(button, buttons.transform);
            answerBtn.GetComponentInChildren<Text>().text = i;
            answerBtn.GetComponent<Button>().onClick.AddListener(() => SendMessage(i));
        }
    }

    private IEnumerator SetupDuel()
    {
        yield return new WaitForSeconds(1.5f);
        CloseChat();
        Main.IsChatOpened = false;
        Main.IsFormsOpened = true;
        Main.IsCallingOpen = true;
        Main.IsSwipesFrozen = true;

        if (Main.IsFirstDuel)
        {
            Main.DuelTutorial.SetActive(Main.IsFirstDuel);
            Main.IsFirstDuel = false;
        }
    }
}