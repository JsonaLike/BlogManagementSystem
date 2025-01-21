import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { PostsRoutingModule } from './posts-routing.module';
import { EditorModule } from 'primeng/editor';
import { AddPostComponent } from './add-post/add-post.component';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { PostsComponent } from './posts/posts.component';
import { TableModule } from 'primeng/table';
import { PostViewComponent } from './post-view/post-view.component';
import { MultiSelectModule } from 'primeng/multiselect';
@NgModule({
  declarations: [PostsComponent, AddPostComponent, PostViewComponent],
  imports: [
    CommonModule,
    PostsRoutingModule,
    EditorModule,
    FormsModule,
    ButtonModule,
    TableModule,
    InputTextModule,
    MultiSelectModule
  ]
})
export class PostsModule { }
