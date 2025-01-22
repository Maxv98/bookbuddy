import { useBookbuddy } from '../composables/useBookbuddy';
import { API_URL } from '../../config';

global.fetch = jest.fn();

describe('useBookbuddy', () => {
    beforeEach(() => {
        (fetch as jest.Mock).mockClear();
    });

    it('registerBookbuddy - success', async () => {
        const { registerBookbuddy } = useBookbuddy();
        const mockBookbuddy = { id: 1, username: 'testuser', email: 'test@example.com', password: 'password' };
        const mockResponse = 1;

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: true,
            json: async () => mockResponse,
        });

        const result = await registerBookbuddy(mockBookbuddy);
        expect(result).toEqual(mockResponse);
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Bookbuddy/Register`, expect.any(Object));
    });

    it('registerBookbuddy - error', async () => {
        const { registerBookbuddy } = useBookbuddy();
        const mockBookbuddy = { id: 1, username: 'testuser', email: 'test@example.com', password: 'password' };

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: false,
            status: 409,
            text: async () => 'Conflict error',
        });

        await expect(registerBookbuddy(mockBookbuddy)).rejects.toThrow('Conflict error');
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Bookbuddy/Register`, expect.any(Object));
    });

    it('fetchBookbuddyById - success', async () => {
        const { fetchBookbuddyById } = useBookbuddy();
        const mockResponse = { id: 1, username: 'testuser', email: 'test@example.com' };

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: true,
            json: async () => mockResponse,
        });

        const result = await fetchBookbuddyById(1);
        expect(result).toEqual(mockResponse);
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Bookbuddy/1`);
    });

    it('fetchBookbuddyById - error', async () => {
        const { fetchBookbuddyById } = useBookbuddy();

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: false,
            statusText: 'Not Found',
        });

        await expect(fetchBookbuddyById(1)).rejects.toThrow('Failed to fetch bookbuddy: Not Found');
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Bookbuddy/1`);
    });

    it('fetchBookbuddies - success', async () => {
        const { fetchBookbuddies } = useBookbuddy();
        const mockResponse = [{ id: 1, username: 'testuser', email: 'test@example.com' }];

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: true,
            json: async () => mockResponse,
        });

        const result = await fetchBookbuddies();
        expect(result).toEqual(mockResponse);
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Bookbuddy/Index`);
    });

    it('fetchBookbuddies - error', async () => {
        const { fetchBookbuddies } = useBookbuddy();

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: false,
            statusText: 'Not Found',
        });

        await expect(fetchBookbuddies()).rejects.toThrow('Failed to fetch bookbuddies: Not Found');
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Bookbuddy/Index`);
    });

    it('updateBookbuddy - success', async () => {
        const { updateBookbuddy } = useBookbuddy();
        const mockBookbuddy = { id: 1, username: 'testuser', email: 'test@example.com', password: 'password' };
        const mockResponse = { id: 1, username: 'testuser', email: 'test@example.com' };

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: true,
            json: async () => mockResponse,
        });

        const result = await updateBookbuddy(mockBookbuddy);
        expect(result).toEqual(mockResponse);
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Bookbuddy/Update`, expect.any(Object));
    });

    it('updateBookbuddy - error', async () => {
        const { updateBookbuddy } = useBookbuddy();
        const mockBookbuddy = { id: 1, username: 'testuser', email: 'test@example.com', password: 'password' };

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: false,
            status: 409,
            text: async () => 'Conflict error',
        });

        await expect(updateBookbuddy(mockBookbuddy)).rejects.toThrow('Conflict error');
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Bookbuddy/Update`, expect.any(Object));
    });

    it('deleteBookbuddy - success', async () => {
        const { deleteBookbuddy } = useBookbuddy();
        const mockBookbuddy = { id: 1, username: 'testuser', email: 'test@example.com', password: 'password' };
        const mockResponse = { message: 'Delete successful' };

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: true,
            json: async () => mockResponse,
        });

        const result = await deleteBookbuddy(mockBookbuddy);
        expect(result).toEqual(mockResponse);
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Bookbuddy/Delete`, expect.any(Object));
    });

    it('deleteBookbuddy - error', async () => {
        const { deleteBookbuddy } = useBookbuddy();
        const mockBookbuddy = { id: 1, username: 'testuser', email: 'test@example.com', password: 'password' };

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: false,
            status: 409,
            text: async () => 'Conflict error',
        });

        await expect(deleteBookbuddy(mockBookbuddy)).rejects.toThrow('Conflict error');
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Bookbuddy/Delete`, expect.any(Object));
    });

    it('savePost - success', async () => {
        const { savePost } = useBookbuddy();
        const username = 'testuser';
        const postId = 1;

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: true,
        });

        await savePost(username, postId);
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Bookbuddy/SavePost`, expect.any(Object));
    });

    it('savePost - error', async () => {
        const { savePost } = useBookbuddy();
        const username = 'testuser';
        const postId = 1;

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: false,
            status: 409,
            text: async () => 'Conflict error',
        });

        await expect(savePost(username, postId)).rejects.toThrow('Conflict error');
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Bookbuddy/SavePost`, expect.any(Object));
    });
});