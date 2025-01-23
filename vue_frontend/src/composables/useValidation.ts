import { API_URL } from "../../config";

export const useValidation = () => {
  const validateUsername = async (value: string): Promise<string | null> => {
    if (value.length === 0) {
        return "Username is required";
      }
    try {
      const response = await fetch(
        `${API_URL}/Bookbuddy/CheckUsername?username=${value}`);
      if (!response.ok) {
        return "Username already exists";
      }
      return null;
    } catch (error: any) {}
  };

  const validateEmail = async (value: string): Promise<string | null> => {
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (value.length === 0) {
      return "Email is required";
    }
    if (!emailPattern.test(value)) {
      return "Invalid email address";
    }

    try {
      const response = await fetch(`${API_URL}/Bookbuddy/CheckEmail?email=${value}`);
      if (!response.ok) {
        return "Email already exists";
      }
      return null;
    } catch (error: any) {}
  };

  const validatePassword = async (value: string): Promise<string | null> => {
    if (value.length >= 6) {
      return null;
    }
    return "Password must be at least 6 characters long";
  };

  const validateConfirmPassword = async (
    password1: string,
    password2: string
  ): Promise<string | null> => {
    if (password1 === password2) {
      return null;
    }
    return "Passwords do not match";
  };

  return {
    validateEmail,
    validatePassword,
    validateConfirmPassword,
    validateUsername,
  };
};
