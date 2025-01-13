import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { TableModule } from 'primeng/table';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { CategoriesComponent } from './app-dashboard-layout/categories/categories/categories.component';
import { PostsComponent } from './app-dashboard-layout/posts/posts/posts.component';

@NgModule({
  declarations: [],
  imports: [
    RouterModule, 
    CommonModule,
    TableModule,
    DashboardRoutingModule
  ]
})
export class DashboardModule { }
