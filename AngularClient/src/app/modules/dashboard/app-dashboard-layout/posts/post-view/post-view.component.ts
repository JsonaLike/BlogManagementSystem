import { Component } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-post-view',
  standalone: false,
  templateUrl: './post-view.component.html',
  styleUrl: './post-view.component.scss'
})
export class PostViewComponent {
  content:string='';
  title:string='';
  postId: string='';
constructor(
  private postService:PostsService,
  public router:Router,
  private route:ActivatedRoute,
  private messageService: MessageService){}
ngOnInit(): void {
  this.postId = (this.route.snapshot.paramMap.get('id') as string);
  this.postService.getPostById(this.postId).subscribe(x=>{
    this.content = x.content;
    this.title = x.title;
  });
}
onSubmit(){
  this.postService.updatePost(this.postId, {title:this.title, content:this.content}).subscribe(x=>{
  this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Post updated Successfully', life: 3000 });
    this.router.navigate(['/dashboard/posts']);
  });
}
}
