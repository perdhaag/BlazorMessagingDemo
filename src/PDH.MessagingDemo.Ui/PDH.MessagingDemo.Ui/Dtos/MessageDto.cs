namespace PDH.MessagingDemo.Ui.Dtos;

public class MessageDto
{
    public string Message { get; set; } = null!;

    public string User { get; set; } = null!;

    public bool Mine { get; set; }

    public bool IsNotice => Message.StartsWith("[Notice]");

    public string Css => Mine ? "sent" : "received";
}
