import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-category',
  standalone: false,
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.scss'
})
export class AddCategoryComponent {
value!: string;
constructor(public router : Router){}
}
