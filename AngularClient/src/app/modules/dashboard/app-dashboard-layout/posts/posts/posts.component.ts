import { Component } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { Post } from '../../models/post.model';
import { Router } from '@angular/router';
import { PageListModel } from '../../../../shared/models/page-list.model';
import { SearchCriteriaBase } from '../../../../shared/models/search-criteria-base.model';

@Component({
  selector: 'app-posts',
  standalone:false,
  templateUrl: './posts.component.html',
  styleUrl: './posts.component.scss'
})
export class PostsComponent {
  posts: PageListModel<Post>;
  searchCriteria:SearchCriteriaBase;
  constructor(private postsService: PostsService,public router:Router) {}

  ngOnInit(): void {
    this.searchCriteria = { pageSize: 10, pageNumber: 1 };
    this.loadPosts();
  }
  loadPosts(){
    this.postsService.getPosts(this.searchCriteria).subscribe({
      next: (response) => {
        this.posts = response;
      },
      error: (err) => console.error('Error loading posts:', err)
    });
  }
  onPageChange(event: any): void {
    console.log('Page changed:', event);
    const newPage = event.page + 1;
    const pageSize = event.rows;
    this.searchCriteria = {pageSize:pageSize,pageNumber:newPage}
    this.loadPosts();
  }
}
