import { ref } from 'vue';
import { API_URL } from '../../config';

export interface Post {
    id: number;
    bookbuddyUsername: string;
    title: string;
    text: string;
}

export const usePost = () => {
    const createPost = async (post: Post): Promise<any> => {
        try {
            const response = await fetch(`${API_URL}/Posts/Create`, 
                { body : JSON.stringify(post), 
                    method: 'POST', 
                    headers: { 'Content-Type': 'application/json' }}
            );

            if (!response.ok) {
                const errorData = ref(null);
                if (response.status === 409) {
                    errorData.value = await response.text();
                }

                console.error('Server error response:', errorData);
                throw new Error(errorData.value || 'An error occurred while creating the post');
            }

            const data = await response.json();
            console.log('Post posted succesfully', data);
            return data;
        } catch (error) {
            console.error('Error:', error);
            throw error;
        }
    };

    const fetchPostById = async (id: number) => {
        try {
            console.log(`${API_URL}/Posts/?id=${id}`);
            const response = await fetch(`${API_URL}/Posts/${id}`);

            if (!response.ok) {
                throw new Error(`Failed to fetch post: ${response.statusText}`);
            }
            
            return await response.json() as Post;

        } catch (error: any) {
            throw new Error(error.message || "Error fetching post.");
        }
    };

    const fetchPostsByBookbuddy = async (id: number) => {
        try {
            const response = await fetch(`${API_URL}/Posts/ByBookbuddy/${id}`);

            if (!response.ok) {
                throw new Error(`Failed to fetch posts: ${response.statusText}`);
            }

            return await response.json() as Post[];
        } catch (error: any) {
            throw new Error(error.message || "Error fetching posts.");
        } 
    };

    const fetchPostsSavedByBookbuddy = async (id: number) => {
        try {
            const response = await fetch(`${API_URL}/Posts/SavedByBookbuddy/${id}`);

            if (!response.ok) {
                throw new Error(`Failed to fetch posts: ${response.statusText}`);
            }

            return await response.json() as Post[];
        } catch (error: any) {
            throw new Error(error.message || "Error fetching posts.");
        } 
    };

    const fetchPosts = async () => {
        try {
            const response = await fetch(`${API_URL}/Posts/Index`);

            if (!response.ok) {
                throw new Error(`Failed to fetch posts: ${response.statusText}`);
            }

            return await response.json() as Post[];
        } catch (error: any) {
            throw new Error(error.message || "Error fetching posts.");
        }
    };

    return {
        createPost,
        fetchPostById,
        fetchPostsByBookbuddy,
        fetchPostsSavedByBookbuddy,
        fetchPosts,
    }
}