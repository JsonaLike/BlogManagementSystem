import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoriesService } from '../../services/categories-service.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-add-category',
  standalone: false,
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.scss',
})
export class AddCategoryComponent {
  value!: string;
  catId: string;
  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private categoryService: CategoriesService,
    private messageService: MessageService
  ) {}
  ngOnInit(): void {
    this.catId = this.route.snapshot.paramMap.get('id') as string;
    if (this.catId) {
      this.categoryService.getCategoryById(this.catId).subscribe((x) => {
        this.value = x.name;
      });
    }
  }
  onSubmit() {
   if(this.catId){
    this.categoryService.updateCategory(this.catId, {name:this.value,description:''}).subscribe(x=>{
      this.messageService.add({ 
        severity: 'success', 
        summary: 'Success', 
        detail: 'Category updated Successfully', 
        life: 3000 
      });
      this.router.navigate(['/dashboard/categories']);
    });
   } 
   else{
    this.categoryService.createCategory({name:this.value,description:''}).subscribe(x=>{
      this.messageService.add({
        severity: 'success', 
        summary: 'Success', 
        detail: 'Category Added Successfully', 
        life: 3000 
      });
      this.router.navigate(['/dashboard/categories']);
    });
   }
  }
}
