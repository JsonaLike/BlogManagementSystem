import { Component } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import plugin from 'grapesjs-blocks-basic';
import { CategoriesService } from '../../services/categories-service.service';
import { Category } from '../../models/category.model';
declare var grapesjs: any;

@Component({
  selector: 'app-post-view',
  standalone: false,
  templateUrl: './post-view.component.html',
  styleUrl: './post-view.component.scss'
})
export class PostViewComponent {
  public editor:any = null;
  content:string='';
  title:string='';
  postId: string='';
  src: string='';
  file: File | null = null; 
  categories: Category[]=[];
  selectedCategories: Category[]=[];
constructor(
  private postService:PostsService,
  public router:Router,
  private route:ActivatedRoute,
  private messageService: MessageService,
 private categoriesService: CategoriesService){}
ngOnInit(): void {
  this.postId = (this.route.snapshot.paramMap.get('id') as string);
  this.postService.getPostById(this.postId).subscribe(x=>{
    this.content = x.content;
    this.title = x.title;
    this.src = 'data:image/png;base64,' + x.image;
    this.categoriesService.getCategories().subscribe(y=>{
      this.categories = y;
      this.selectedCategories = [];
      x.categories.forEach((cat) => {
        const categoryToPush = this.categories.find((category) => category.id === cat.id);
        if (categoryToPush) {
          this.selectedCategories.push(categoryToPush);
        }
      });
    });

  });
  
}
onFileChange(event: Event): void {
  const input = event.target as HTMLInputElement;
  if (input.files && input.files[0]) {
    this.file = input.files[0];
    const reader = new FileReader();
    reader.onload = (e: any) => {
      this.src = e.target.result;
    };
    reader.readAsDataURL(input.files[0]);
  }
} 
onSubmit(): void {
  const formData = new FormData();
  formData.append('title', this.title);
  formData.append('content', this.content);
  this.selectedCategories.forEach((category) => {
    formData.append('categories', category.id);
});
if (this.file) {
    formData.append('image', this.file, this.file.name);
  }

  this.postService.updatePost(this.postId, formData).subscribe(() => {
    this.messageService.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Post updated successfully',
      life: 3000
    });
    this.router.navigate(['/dashboard/posts']);
  });
}
convertToBase64(imageBytes: Uint8Array): Promise<string> {
  return new Promise((resolve, reject) => {
    const blob = new Blob([imageBytes], { type: 'image/jpeg' }); // Adjust MIME type if necessary
    const reader = new FileReader();

    reader.onloadend = () => {
      resolve(reader.result?.toString().split(',')[1] ?? ''); // Extract base64 content
    };
    reader.onerror = error => reject(error);

    reader.readAsDataURL(blob);
  });
}
}
