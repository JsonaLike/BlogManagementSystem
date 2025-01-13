import { NgModule } from '@angular/core';
import { AppDashboardLayoutComponent } from './app-dashboard-layout/app-dashboard-layout.component';
import { HomeComponent } from './app-dashboard-layout/pages/home/home.component';
import { OverviewComponent } from './app-dashboard-layout/pages/overview/overview.component';
import { RouterModule, Routes } from '@angular/router';
import { AppSidebarComponent } from './app-dashboard-layout/app-sidebar/app-sidebar.component';
import { CommonModule } from '@angular/common';
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
      loadChildren: () =>
        import('../dashboard/app-dashboard-layout/posts/posts.module').then(
          m => m.PostsModule,
        ),
    },
    {
      path: 'categories',
      loadChildren: () =>
        import('../dashboard/app-dashboard-layout/categories/categories.module').then(
          m => m.CategoriesModule,
        ),
    },
    {
      path: 'settings',
      loadChildren: () =>
        import('../dashboard/app-dashboard-layout/settings/settings.module').then(
          m => m.SettingsModule,
        ),
    }

  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes), CommonModule],
  exports: [RouterModule],
  declarations : [AppDashboardLayoutComponent,AppSidebarComponent,OverviewComponent]
})
export class DashboardRoutingModule { }
