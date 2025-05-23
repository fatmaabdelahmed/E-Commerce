import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  photos: Photo[];
  categoryName: string;
}

export interface Photo {
  imageName: string;
  productId: number;
}

export interface AddProduct {
  name: string;
  description: string;
  newPrice: number;
  oldPrice: number;
  categoryId: number;
  photo: FileList;
}

export interface UpdateProduct extends AddProduct {
  id: number;
}

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = `${environment.apiUrl}/products`;

  constructor(private http: HttpClient) {}

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/get-all`);
  }

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/get-by-id/${id}`);
  }

  addProduct(productData: AddProduct): Observable<any> {
    const formData = new FormData();
    formData.append('name', productData.name);
    formData.append('description', productData.description);
    formData.append('newPrice', productData.newPrice.toString());
    formData.append('oldPrice', productData.oldPrice.toString());
    formData.append('categoryId', productData.categoryId.toString());
    
    if (productData.photo) {
      for (let i = 0; i < productData.photo.length; i++) {
        formData.append('photo', productData.photo[i]);
      }
    }

    return this.http.post(`${this.apiUrl}/add`, formData);
  }

  updateProduct(productData: UpdateProduct): Observable<any> {
    const formData = new FormData();
    formData.append('id', productData.id.toString());
    formData.append('name', productData.name);
    formData.append('description', productData.description);
    formData.append('newPrice', productData.newPrice.toString());
    formData.append('oldPrice', productData.oldPrice.toString());
    formData.append('categoryId', productData.categoryId.toString());
    
    if (productData.photo) {
      for (let i = 0; i < productData.photo.length; i++) {
        formData.append('photo', productData.photo[i]);
      }
    }

    return this.http.put(`${this.apiUrl}/update`, formData);
  }

  deleteProduct(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/delet/${id}`);
  }

  getImageUrl(imageName: string): string {
    return `${environment.apiUrl}/${imageName}`;
  }
}