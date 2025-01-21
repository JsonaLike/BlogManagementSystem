import {Component} from '@angular/core'; 
import { PostsService } from '../../services/posts.service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { Category } from '../../models/category.model';
import { CategoriesService } from '../../services/categories-service.service';
@Component({
  selector: 'app-add-post',
  standalone: false,
  templateUrl: './add-post.component.html',
  styleUrl: './add-post.component.scss'
})
export class AddPostComponent {
  constructor(private postService:PostsService,
    private messageService: MessageService,
    public router:Router,
     private categoriesService: CategoriesService
  ){}

  categories: Category[]=[];
  text:string = '';
  title:string = '';
  src: string='';
  file: File | null = null; 
  selectedCategories: Category[]=[];
  ngOnInit(): void {
    this.categoriesService.getCategories().subscribe(y=>{
      this.categories = y;
  });
  
  }
  onSubmit() {
    const formData = new FormData();
    formData.append('title', this.title);
    formData.append('content', this.text);
    formData.append('image', this.file as File);
    this.selectedCategories.forEach((category) => {
      formData.append('categories', category.id);
  });
    this.postService.createPost(formData).subscribe(x=>{
        this.messageService.add({ 
          severity: 'success', 
          summary: 'Success', 
          detail: 'Post Added Successfully', 
          life: 3000 
        });
        this.router.navigate(['/dashboard/posts']);
      }
    );
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
}