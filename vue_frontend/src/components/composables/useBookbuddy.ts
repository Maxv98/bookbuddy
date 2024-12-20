import { ref } from 'vue';
import { API_URL } from '../../../config';

export interface Bookbuddy {
    id: number;
    username: string;
    email: string;
    password: string;
}

export const useBookbuddy = () => {
    const bookbuddy = ref<Bookbuddy | null>(null);

    const registerBookbuddy = async (bookbuddy: Bookbuddy): Promise<any> => {
        try {
            console.log(JSON.stringify(bookbuddy, null, 2))
            console.log(`${API_URL}/Bookbuddy/Register`);
            const response = await fetch(`${API_URL}/Bookbuddy/Register`, 
                { body : JSON.stringify(bookbuddy), 
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
            console.log('Registration successful', data);
            return data;
        } catch (error) {
            console.error('Error:', error);
            throw error;
        }
    };

    const fetchBookbuddyById = async (id: number) => {
        try {
            console.log(`${API_URL}/Bookbuddy/?id=${id}`);
            const response = await fetch(`${API_URL}/Bookbuddy/${id}`);

            if (!response.ok) {
                throw new Error(`Failed to fetch bookbuddy: ${response.statusText}`);
            }
            bookbuddy.value = await response.json();
            return bookbuddy.value;

        } catch (error: any) {
            throw new Error(error.message || "Error fetching bookbuddy.");
        }
    };

    const fetchBookbuddies = async () => {
        try {
            const response = await fetch(`${API_URL}/Bookbuddy/Index`);

            if (!response.ok) {
                throw new Error(`Failed to fetch bookbuddies: ${response.statusText}`);
            }

            return await response.json() as Bookbuddy[];
        } catch (error: any) {
            throw new Error(error.message || "Error fetching bookbuddies.");
        }
    };

    const updateBookbuddy = async (bookbuddy: Bookbuddy) => {
        try {
            const response = await fetch(`${API_URL}/Bookbuddy/Update`, {
                method: 'PUT',
                body: JSON.stringify(bookbuddy),
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (!response.ok) {
                const errorData = ref(null);
                if (response.status === 409) {
                    errorData.value = await response.text();
                }

                console.error('Server error response:', errorData); // Log server error response
                throw new Error(errorData.value || 'An error occurred while updating the book buddy');
            }

            const data = await response.json();
            console.log('Update successful', data);
            return data;

        } catch (error) {
            console.error('Error:', error);
            throw error;
        }
    };

    const deleteBookbuddy = async (bookbuddy: Bookbuddy) => {
        try {
            const response = await fetch(`${API_URL}/Bookbuddy/Delete`, {
                method: 'DELETE',
                body: JSON.stringify(bookbuddy),
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (!response.ok) {
                const errorData = ref(null);
                if (response.status === 409) {
                    errorData.value = await response.text();
                }

                console.error('Server error response:', errorData); // Log server error response
                throw new Error(errorData.value || 'An error occurred while deleting the book buddy');
            }
            const data = await response.json();
            console.log('Delete successful', data);
            return data;

        } catch (error) {
            console.error('Error:', error);
            throw error;
        }
    };

    return {
        bookbuddy,
        registerBookbuddy,
        fetchBookbuddyById,
        fetchBookbuddies,
        updateBookbuddy,
        deleteBookbuddy,
    }
}