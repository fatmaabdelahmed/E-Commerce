import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Category {
  id: number;
  name: string;
  description: string;
}

export interface CategoryDto {
  name: string;
  description: string;
}

export interface UpdateCategoryDto extends CategoryDto {
  id: number;
}

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = `${environment.apiUrl}/categories`;

  constructor(private http: HttpClient) {}

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.apiUrl}/get-all`);
  }

  getCategoryById(id: number): Observable<Category> {
    return this.http.get<Category>(`${this.apiUrl}/get-b-id/${id}`);
  }

  addCategory(category: CategoryDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/add-category`, category);
  }

  updateCategory(category: UpdateCategoryDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/update-category`, category);
  }

  deleteCategory(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/delete-category?id=${id}`);
  }
}