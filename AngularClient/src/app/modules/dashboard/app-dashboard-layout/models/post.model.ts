import { Category } from "./category.model";

export interface Post {
    id: string;
    title: string;
    content: string;
    status: string;
    image: string | ArrayBuffer | null;
    authorId: string;
    categories : Category[];
    imageBase64: string;
  }