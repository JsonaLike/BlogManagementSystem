import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../../environments/environment';
import { Category } from '../models/category.model';
import { CreateUpdateCategoryDto } from '../models/createUpdateCategory';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
  baseApi = `${environment.baseApiUrl}/api/category`;

  constructor(private http: HttpClient) { }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseApi);
  }

  getCategoryById(id: string): Observable<Category> {
    return this.http.get<Category>(`${this.baseApi}/${id}`);
  }

  createCategory(dto: CreateUpdateCategoryDto): Observable<Category> {
    return this.http.post<Category>(this.baseApi, dto);
  }

  updateCategory(id: string, dto: CreateUpdateCategoryDto): Observable<void> {
    return this.http.put<void>(`${this.baseApi}/${id}`, dto);
  }

  deleteCategory(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseApi}/${id}`);
  }

  assignPostToCategory(categoryId: string, postId: string): Observable<void> {
    return this.http.post<void>(`${this.baseApi}/${categoryId}/posts/${postId}`, {});
  }

  removePostFromCategory(categoryId: string, postId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseApi}/${categoryId}/posts/${postId}`);
  }
}
