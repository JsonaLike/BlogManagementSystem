import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoriesRoutingModule } from './categories-routing.module';
import { ButtonModule } from 'primeng/button';
import { CategoriesComponent } from './categories/categories.component';
import { TableModule } from 'primeng/table';
import { AddCategoryComponent } from './add-category/add-category.component';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';

@NgModule({
  declarations: [CategoriesComponent, AddCategoryComponent],
  imports: [
    CommonModule,
    CategoriesRoutingModule,
    ButtonModule,
    TableModule,
    FormsModule,
    InputTextModule
  ]
})
export class CategoriesModule { }
