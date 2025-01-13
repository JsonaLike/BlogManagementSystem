import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchCriteriaBase } from '../../../shared/models/search-criteria-base.model';
import { PageListModel } from '../../../shared/models/page-list.model';
import { Category } from '../models/category.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
  baseApi = environment.baseApiUrl;
  constructor(private http:HttpClient) { }
  getCategories(): Observable<Category[]> {
		return this.http.get<Category[]>(this.baseApi+`/category`);
	}

}
