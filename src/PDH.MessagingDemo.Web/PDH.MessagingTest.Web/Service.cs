using PDH.MessagingDemo.Web.Client;

namespace PDH.MessagingDemo.Web
{
    public class Service
    {
        private readonly HttpClient _client;

        public Service(HttpClient client)
        {
            _client = client;
        }

        public async Task SendMessage(TestMessageReceivedEventArgs message)
        {
            await _client.PostAsync($"message?message={message.Message}&user={message.User}", null);
        }
    }
}
