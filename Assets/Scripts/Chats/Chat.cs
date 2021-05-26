using System.Collections.Generic;
using System.Linq;

public class Chat
{
    private string partner;
    private List<Message> messages;

    public string Partner { get => partner; }
    public List<Message> Messages { get => messages; }

    public Message LastMessage { get => messages.Last(); }

    public Chat(string partner)
    {
        this.partner = partner;
        messages = new List<Message>();
    }

    public Message SendMessage(string author, string sentence)
    {
        var message = new Message(author, sentence);
        messages.Add(message);
        return message;
    }
}

public class Message
{
    private string author, sentence;

    public string Author { get => author; }
    public string Sentence { get => sentence; }

    public Message(string author, string sentence)
    {
        this.author = author;
        this.sentence = sentence;
    }
}
