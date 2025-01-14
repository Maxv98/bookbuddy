using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent;
using Logic.Entities;
using System.Text.Json;

namespace WebAPI
{
    public static class WebSocketHandler
    {
        private static readonly ConcurrentDictionary<WebSocket, bool> WebSockets = new ConcurrentDictionary<WebSocket, bool>();

        public static async Task HandleWebSocketAsync(WebSocket webSocket)
        {
            try
            {
                WebSockets.TryAdd(webSocket, true);
                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult result;

                do
                {
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }
                while (!result.CloseStatus.HasValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling WebSocket: {ex.Message}");
            }
            finally
            {
                RemoveAndCloseWebSocket(webSocket);
            }
        }

        public static async Task NotifyClientsAsync(string message)
        {
            var toRemove = new List<WebSocket>();

            foreach (var socket in WebSockets.Keys)
            {
                if (socket.State == WebSocketState.Open)
                {
                    try
                    {
                        var bytes = Encoding.UTF8.GetBytes(message);
                        await socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending message to WebSocket: {ex.Message}");
                        toRemove.Add(socket);
                    }
                }
                else
                {
                    toRemove.Add(socket);
                }
            }

            foreach (var socket in toRemove)
            {
                RemoveAndCloseWebSocket(socket);
            }
        }

        public static async Task NotifyClientsWithPostAsync(Post post)
        {
            Console.WriteLine($"Notifying clients with post. Bookbuddy Id: {post.BookbuddyId}");
            var message = JsonSerializer.Serialize(post);
            await NotifyClientsAsync(message);
        }

        private static void RemoveAndCloseWebSocket(WebSocket webSocket)
        {
            if (WebSockets.TryRemove(webSocket, out _))
            {
                try
                {
                    if (webSocket.State != WebSocketState.Closed && webSocket.State != WebSocketState.Aborted)
                    {
                        webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None).Wait();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    webSocket.Dispose();
                }
            }
        }
    }
}
