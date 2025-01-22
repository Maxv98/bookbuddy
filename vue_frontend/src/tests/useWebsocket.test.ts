import { onUnmounted, createApp } from 'vue';
import { useWebSocket } from '../composables/useWebSocket';
import WebSocketServer from 'jest-websocket-mock';

describe('useWebSocket', () => {
    let mockServer: WebSocketServer;

    beforeEach(() => {
        mockServer = new WebSocketServer('ws://localhost:8080/ws');
    });

    afterEach(() => {
        mockServer.close();
    });

    it('should connect to WebSocket server', async () => {
        const { isConnected } = useWebSocket();
        expect(isConnected.value).toBe(false);

        await mockServer.connected;
        expect(isConnected.value).toBe(true);
    });

    it('should receive messages from WebSocket server', async () => {
        const { newMessage } = useWebSocket();
        const testMessage = 'Hello, WebSocket!';

        mockServer.send(testMessage);
        await new Promise(resolve => setTimeout(resolve, 100)); // Wait for the message to be processed
        expect(newMessage.value).toBe(testMessage);
    });

    it('should handle WebSocket errors', async () => {
        const consoleErrorSpy = jest.spyOn(console, 'error').mockImplementation(() => {});
        const { isConnected } = useWebSocket();

        mockServer.error();
        await new Promise(resolve => setTimeout(resolve, 100)); // Wait for the error to be processed
        expect(consoleErrorSpy).toHaveBeenCalledWith('WebSocket error:', expect.objectContaining({
            type: 'error',
            target: expect.any(WebSocket),
        }));
        consoleErrorSpy.mockRestore();
    });

    it('should handle WebSocket close', async () => {
        const { isConnected } = useWebSocket();
        await mockServer.connected;
        expect(isConnected.value).toBe(true);

        mockServer.close();
        await new Promise(resolve => setTimeout(resolve, 100)); // Wait for the close event to be processed
        expect(isConnected.value).toBe(false);
    });

    it('should close WebSocket on unmount', async () => {
        const closeSpy = jest.spyOn(WebSocket.prototype, 'close');

        const app = createApp({
            setup() {
                const { socket } = useWebSocket();
                onUnmounted(() => {
                    socket.close();
                });
            },
            template: '<div></div>',
        });

        const vm = app.mount(document.createElement('div'));
        app.unmount();

        await new Promise(resolve => setTimeout(resolve, 100)); // Wait for the unmount to be processed
        expect(closeSpy).toHaveBeenCalled();
        closeSpy.mockRestore();
    });
});