import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../../environments/environment';
import { Post } from '../models/post.model';
import { SearchCriteriaBase } from '../../../shared/models/search-criteria-base.model';

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  private readonly apiUrl = environment.baseApiUrl + '/post';

  constructor(private http: HttpClient) {}

  getPosts(searchCriteria: SearchCriteriaBase): Observable<{ items: Post[]; totalCount: number }> {
    let params = new HttpParams();
    Object.keys(searchCriteria).forEach(key => {
      const value = searchCriteria[key as keyof SearchCriteriaBase];
      if (value !== undefined && value !== null) {
        params = params.append(key, value.toString());
      }
    });
  
    return this.http.get<{ items: Post[]; totalCount: number }>(this.apiUrl, { params });
  }
  

  getPostById(id: string): Observable<Post> {
    return this.http.get<Post>(`${this.apiUrl}/${id}`);
  }

  createPost(post: Post): Observable<any> {
    return this.http.post<any>(this.apiUrl, post);
  }

  updatePost(id: string, post: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, post);
  }

  deletePost(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
