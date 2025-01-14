import { ref, onUnmounted, reactive } from 'vue';

export const useWebSocket = () => {
  const socket = new WebSocket("ws://localhost:8080/ws");
  const newMessage = ref("");
  const isConnected = ref(false);

  socket.onopen = () => {
    console.log("WebSocket connected!");
    isConnected.value = true;
  };

  socket.onmessage = (message) => {
    console.log("WebSocket message received:", message.data);
    newMessage.value = message.data;
  };

  socket.onerror = (error) => {
    console.error("WebSocket error:", error);
  };

  socket.onclose = () => {
    console.log("WebSocket connection closed.");
    isConnected.value = false;
  };

  onUnmounted(() => {
    socket.close();
  });

  return {
    socket,
    newMessage,
    isConnected,
  };
};