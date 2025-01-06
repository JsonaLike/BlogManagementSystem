import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdminSidebarItems } from '../../../shared/lists/AdminSidebarItems';

@Component({
  selector: 'app-app-sidebar',
  templateUrl: './app-sidebar.component.html',
  styleUrl: './app-sidebar.component.scss'
})
export class AppSidebarComponent {
  location:string = window.location.href;
  sidebarVisible:boolean=true;
  AdminSidebarItems = AdminSidebarItems;
  constructor(public router:Router){}
  ngOnInit(): void {
    this.addResponsiveListener();
  }
  toggleSidebar() {
    this.sidebarVisible = !this.sidebarVisible;
    var sidebar = document.getElementsByClassName('main-sidebar')[0];
    if (sidebar!.classList.contains('hidden')) {
      sidebar!.classList.remove('hidden');
    } else {
      sidebar!.classList.add('hidden');
    }
  } 
  hideSidebar() {
    this.sidebarVisible = false;
    var sidebar = document.getElementsByClassName('main-sidebar')[0];
    if (!sidebar!.classList.contains('hidden')) {
      sidebar!.classList.add('hidden'); 
    }
  }
  addResponsiveListener() {
    const mediaQuery = window.matchMedia("(min-width: 768px)");
    const handleDeviceChange = (event: MediaQueryListEvent) => {
      if (event.matches) {
        this.showSidebar(); 
      }
    };
    mediaQuery.addEventListener("change", handleDeviceChange);
    if (mediaQuery.matches) {
      this.showSidebar();
    }
  }

showSidebar() {
  this.sidebarVisible = true; 
  const sidebar = document.getElementsByClassName('main-sidebar')[0];
  if (sidebar!.classList.contains('hidden')) {
    sidebar!.classList.remove('hidden'); 
  }
}
toggleExpand(item: any): void {
  item.expanded = !item.expanded;
}
  logout(){
    }
    ParentClick(path:string){
     var sibiling =  document.getElementById(`No${path}`)?.nextElementSibling;
     console.log(sibiling);
     
     if(sibiling != undefined){
      document.getElementById(`No${path}`)!.classList.toggle('rotate');

     sibiling!.classList.toggle("d-none");
     }
     else{
      this.router.navigate([path]);
     }
    }
}
