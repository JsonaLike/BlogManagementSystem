import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PostsComponent } from './posts/posts.component';
import { AddPostComponent } from './add-post/add-post.component';
import { PostViewComponent } from './post-view/post-view.component';

const routes: Routes = [{
  path:'',
  component:PostsComponent
},
{
  path:'add',
  component:AddPostComponent
},
{
  path:'edit/:id',
  component:PostViewComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostsRoutingModule { }
