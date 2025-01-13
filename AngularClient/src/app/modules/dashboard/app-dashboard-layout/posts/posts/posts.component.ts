import { Component } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { Post } from '../../models/post.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-posts',
  standalone:false,
  templateUrl: './posts.component.html',
  styleUrl: './posts.component.scss'
})
export class PostsComponent {
  posts: Post[] = [];

  constructor(private postsService: PostsService,public router:Router) {}

  ngOnInit(): void {
    this.postsService.getPosts({ pageSize: 10, pageNumber: 1 }).subscribe({
      next: (response) => {
        this.posts = response.items;
      },
      error: (err) => console.error('Error loading posts:', err)
    });
  }
}
