import { Component } from '@angular/core';
import { CategoriesService } from '../../services/categories-service.service';
import { Category } from '../../models/category.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-categories',
  standalone : false,
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss'
})
export class CategoriesComponent {
constructor(private categoryService:CategoriesService,public router:Router){}
categories: Category[]=[];
ngOnInit(): void {
  this.categoryService.getCategories().subscribe(x=>{
    this.categories = x;
  });
}
}
