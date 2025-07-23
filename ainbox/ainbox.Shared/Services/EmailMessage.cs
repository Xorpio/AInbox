using MailKit;

namespace ainbox.Shared.Services;

public class EmailMessage
{
    public required string Subject { get; set; }
    public required string Sender { get; set; }
    public required DateTimeOffset Date { get; set; }
    public required UniqueId UniqueId { get; set; }
    public required string Preview { get; set; }
}
