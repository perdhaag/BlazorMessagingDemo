using PDH.MessagingDemo.Ui.Dtos;
using System.Text.Json;

namespace PDH.MessagingDemo.Ui
{
    public class ChatService
    {
        private readonly HttpClient _client;

        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ChatService(HttpClient client)
        {
            _client = client;
        }

        public async Task CreateUser(string providerId, string userName, string? email)
        {
            await _client.PostAsJsonAsync(
                "user",
                new { ProviderId = providerId, UserName = userName, Email = email ?? string.Empty },
                _serializerOptions);
        }

        public async Task SendMessage(string chatRoomId, string message, string user)
        {
            await _client.PostAsJsonAsync("message", new { Id = chatRoomId, Message = message, User = user }, _serializerOptions);
        }

        public async Task<IEnumerable<string>?> GetUsers()
        {
            return await _client.GetFromJsonAsync<IEnumerable<string>>("user");
        }

        public async Task<List<MessageDto>?> GetChatroomMessages(string chatroomId)
        {
            return await _client.GetFromJsonAsync<List<MessageDto>>($"chatroom/{chatroomId}");
        }

        public async Task<Guid> CreateChatRoom(IEnumerable<string> users)
        {
            var response = await _client.PostAsJsonAsync("chatroom", new { Users = users }, _serializerOptions);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Guid>(json, _serializerOptions);
        }
    }
}
