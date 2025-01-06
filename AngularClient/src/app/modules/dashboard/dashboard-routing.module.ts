import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppDashboardLayoutComponent } from './app-dashboard-layout/app-dashboard-layout.component';
import { HomeComponent } from './app-dashboard-layout/pages/home/home.component';
import { AppSidebarComponent } from "./app-dashboard-layout/app-sidebar/app-sidebar.component";
import { CommonModule } from '@angular/common';
import { OverviewComponent } from './app-dashboard-layout/pages/overview/overview.component';
import { PostsComponent } from './app-dashboard-layout/pages/posts/posts.component';
import { CategoriesComponent } from './app-dashboard-layout/pages/categories/categories.component';
const routes: Routes = [{
  path:'',
  component: AppDashboardLayoutComponent,
  children: [
    { path: '', redirectTo: 'overview', pathMatch: 'full' },
    {
      path: 'home',
      component:HomeComponent
    },
    {
      path: 'overview',
      component:OverviewComponent
    },
    {
      path: 'posts',
      component:PostsComponent
    },
    {
      path:'categories',
      component:CategoriesComponent
    }

  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes), RouterModule, CommonModule],
  exports: [RouterModule],
  declarations : [AppDashboardLayoutComponent, AppSidebarComponent]
})
export class DashboardRoutingModule { }
