import { usePost } from '../composables/usePost';
import { API_URL } from '../../config';

global.fetch = jest.fn();

describe('usePost', () => {
    beforeEach(() => {
        (fetch as jest.Mock).mockClear();
    });

    it('fetchPostById - success', async () => {
        const { fetchPostById } = usePost();
        const mockResponse = { id: 1, bookbuddyId: 1, title: 'Test Post', text: 'This is a test post.' };

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: true,
            json: async () => mockResponse,
        });

        const result = await fetchPostById(1);
        expect(result).toEqual(mockResponse);
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Posts/1`);
    });

    it('fetchPostById - error', async () => {
        const { fetchPostById } = usePost();

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: false,
            statusText: 'Not Found',
        });

        await expect(fetchPostById(1)).rejects.toThrow('Failed to fetch post: Not Found');
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Posts/1`);
    });

    it('fetchPostsByBookbuddy - success', async () => {
        const { fetchPostsByBookbuddy } = usePost();
        const mockResponse = [{ id: 1, bookbuddyId: 1, title: 'Test Post', text: 'This is a test post.' }];

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: true,
            json: async () => mockResponse,
        });

        const result = await fetchPostsByBookbuddy(1);
        expect(result).toEqual(mockResponse);
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Posts/ByBookbuddy/1`);
    });

    it('fetchPostsByBookbuddy - error', async () => {
        const { fetchPostsByBookbuddy } = usePost();

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: false,
            statusText: 'Not Found',
        });

        await expect(fetchPostsByBookbuddy(1)).rejects.toThrow('Failed to fetch posts: Not Found');
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Posts/ByBookbuddy/1`);
    });

    it('fetchPostsSavedByBookbuddy - success', async () => {
        const { fetchPostsSavedByBookbuddy } = usePost();
        const mockResponse = [{ id: 1, bookbuddyId: 1, title: 'Saved Post', text: 'This is a saved post.' }];

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: true,
            json: async () => mockResponse,
        });

        const result = await fetchPostsSavedByBookbuddy(1);
        expect(result).toEqual(mockResponse);
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Posts/SavedByBookbuddy/1`);
    });

    it('fetchPostsSavedByBookbuddy - error', async () => {
        const { fetchPostsSavedByBookbuddy } = usePost();

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: false,
            statusText: 'Not Found',
        });

        await expect(fetchPostsSavedByBookbuddy(1)).rejects.toThrow('Failed to fetch posts: Not Found');
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Posts/SavedByBookbuddy/1`);
    });

    it('fetchPosts - success', async () => {
        const { fetchPosts } = usePost();
        const mockResponse = [{ id: 1, bookbuddyId: 1, title: 'Test Post', text: 'This is a test post.' }];

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: true,
            json: async () => mockResponse,
        });

        const result = await fetchPosts();
        expect(result).toEqual(mockResponse);
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Posts/Index`);
    });

    it('fetchPosts - error', async () => {
        const { fetchPosts } = usePost();

        (fetch as jest.Mock).mockResolvedValueOnce({
            ok: false,
            statusText: 'Not Found',
        });

        await expect(fetchPosts()).rejects.toThrow('Failed to fetch posts: Not Found');
        expect(fetch).toHaveBeenCalledWith(`${API_URL}/Posts/Index`);
    });
});