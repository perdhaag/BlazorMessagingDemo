namespace PDH.MessagingDemo.Web.Client;
public class EventDispatcher<T>
{
    public event EventHandler<T>? ReceivedMessage;

    public virtual void MessageReceived(T e)
    {
        var handler = ReceivedMessage;
        handler?.Invoke(this, e);
    }
}


public class TestMessageReceivedEventArgs : EventArgs
{
    public TestMessageReceivedEventArgs(string message, string user, bool? mine = null)
    {
        Message = message;
        User = user;
        Mine = mine ?? true;
    }

    public string Message { get; set; }

    public string User { get; set; }

    public bool Mine { get; set; }

    public bool IsNotice => Message.StartsWith("[Notice]");

    public string Css => Mine ? "sent" : "received";
}