import { ref } from 'vue';
import { API_URL } from '../../config';
import { type Bookbuddy, useBookbuddy } from './useBookbuddy';

const { fetchBookbuddyById } = useBookbuddy();

export interface Post {
    id: number;
    bookbuddyId: number;
    title: string;
    text: string;
}

export const usePost = () => {
    const postPost = async (post: Post): Promise<any> => {
        try {
            console.log(JSON.stringify(post, null, 2))
            console.log(`${API_URL}/Bookbuddy/Register`);
            const response = await fetch(`${API_URL}/Posts/Post`, 
                { body : JSON.stringify(post), 
                    method: 'POST', 
                    headers: { 'Content-Type': 'application/json' }}
            );

            if (!response.ok) {
                const errorData = ref(null);
                if (response.status === 409) {
                    errorData.value = await response.text();
                }

                console.error('Server error response:', errorData); // Log server error response
                throw new Error(errorData.value || 'An error occurred while registering the book buddy');
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
            throw new Error(error.message || "Error fetching bookbuddy.");
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
        postPost,
        fetchPostById,
        fetchPosts,
    }
}