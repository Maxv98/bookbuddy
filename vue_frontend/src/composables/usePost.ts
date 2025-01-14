import { ref } from 'vue';
import { API_URL } from '../../config';
import { useBookbuddy, type Bookbuddy } from './useBookbuddy';

const { fetchBookbuddyById } = useBookbuddy();

export interface Post {
    id: number;
    bookbuddyId: number;
    title: string;
    text: string;
}

export interface ReceivePost {
    Id: number;
    BookbuddyId: number;
    Title: string;
    Text: string;
}

export const usePost = () => {

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
            console.log(`${API_URL}/Posts/?id=${id}`);
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
        fetchPostById,
        fetchPostsByBookbuddy,
        fetchPostsSavedByBookbuddy,
        fetchPosts,
    }
}