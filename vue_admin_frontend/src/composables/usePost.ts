import { ref } from "vue";

export interface Post {
  id: number;
  title: string;
  text: string;
  bookbuddyId: number;
}

export const usePost = () => {
  const createPost = async (post: Post): Promise<any> => {
    try {
      console.log("Creating post:", post);
      const response = await fetch(`http://localhost:8080/Posts/Create`, {
        body: JSON.stringify(post),
        method: "POST",
        headers: { "Content-Type": "application/json" },
      });

      if (!response.ok) {
        const errorData = ref<string | null>(null);
        if (response.status === 409) {
          errorData.value = await response.text();
        }

        console.error("Server error response:", errorData);
        throw new Error(
          errorData.value || "An error occurred while creating the post"
        );
      }
      console.log("Post posted succesfully");
    } catch (error) {
      console.error("Error:", error);
      throw error;
    }
  };

  const fetchPosts = async () => {
    try {
      const response = await fetch(`http://localhost:8080/Posts/Index`);

      if (!response.ok) {
        throw new Error(`Failed to fetch posts: ${response.statusText}`);
      }

      return (await response.json()) as Post[];
    } catch (error: any) {
      throw new Error(error.message || "Error fetching posts.");
    }
  };

  return { createPost, fetchPosts };
};
