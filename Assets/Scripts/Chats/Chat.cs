using System.Collections.Generic;
using System.Linq;

public class Chat
{
    public string Partner { get; }
    public List<Message> Messages { get; }

    public Message LastMessage { get => Messages.Last(); }

    public Chat(string partner)
    {
        Partner = partner;
        Messages = new List<Message>();
    }

    public Message SendMessage(string author, string sentence)
    {
        var message = new Message(author, sentence);
        Messages.Add(message);
        return message;
    }
}

public class Message
{
    public string Author { get; }
    public string Sentence { get; }

    public Message(string author, string sentence)
    {
        Author = author;
        Sentence = sentence;
    }
}
