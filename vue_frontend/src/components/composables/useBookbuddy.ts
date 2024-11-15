import { ref } from 'vue';
import { API_URL } from '../../../config';

export const useBookbuddy = () => {
    const BASE_URL = `${API_URL}/bookbuddy`;

    interface Bookbuddy {
        id: number;
        username: string;
        email: string;
        password: string;
    }

    async function Register(Bookbuddy: Bookbuddy): Promise<any> {
        try {
            console.log('Registering Bookbuddy with data:', Bookbuddy); // Log the request data

            const response = await fetch(`${BASE_URL}/register`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    username: Bookbuddy.username,
                    email: Bookbuddy.email,
                    password: Bookbuddy.password,
                })
            });

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
    }

    async function Get(id: number): Promise<any> {
        const response = await fetch(`${BASE_URL}/${id}`, {
            method: 'Get',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (!response.ok) {
            throw new Error(`Failed to fetch jobs: ${response.statusText}`);
        }
        return await response.json();
    }

    async function Update(Bookbuddy: Bookbuddy): Promise<any> {
        console.log("Updating Bookbuddy");
        console.log(`${API_URL}/Bookbuddy/update`);
        const response = await fetch(`${BASE_URL}/update`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                id: Bookbuddy.id,
                username: Bookbuddy.username,
                email: Bookbuddy.email,
                password: Bookbuddy.password,
            })
        });
        if (!response.ok) {
            const errorData = ref(null);
            if (response.status === 409) {
                errorData.value = await response.text();
            }

            console.error('Server error response:', errorData); // Log server error response
            throw new Error(errorData.value || 'An error occurred while updating the book buddy');
        }
    }

    return {
        Register,
        Get,
        Update,
    }
}