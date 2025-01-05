import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppDashboardLayoutComponent } from './app-dashboard-layout/app-dashboard-layout.component';
import { HomeComponent } from './app-dashboard-layout/home/home.component';
import { AppSidebarComponent } from "./app-dashboard-layout/app-sidebar/app-sidebar.component";
const routes: Routes = [{
  path:'',
  component: AppDashboardLayoutComponent,
  children: [
    {
      path: '',
      component:HomeComponent
    }

  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes), RouterModule],
  exports: [RouterModule],
  declarations : [AppDashboardLayoutComponent, AppSidebarComponent]
})
export class DashboardRoutingModule { }
