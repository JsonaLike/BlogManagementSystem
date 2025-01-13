export interface Post {
    id: string;
    title: string;
    content: string;
    status: string;
    image: string | ArrayBuffer | null;
    authorId: string;
    categories : string[];
    
  }