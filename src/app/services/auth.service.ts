import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';

export interface User {
  username: string;
  email: string;
  roles: string[];
}

export interface LoginRequest {
  username: string;
  password: string;
}

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl;
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {
    this.loadUserFromToken();
  }

  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/auth/login`, credentials)
      .pipe(
        tap(response => {
          this.setToken(response.token);
          this.loadUserFromToken();
        })
      );
  }

  register(userData: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/auth/register`, userData)
      .pipe(
        tap(response => {
          this.setToken(response.token);
          this.loadUserFromToken();
        })
      );
  }

  logout(): void {
    localStorage.removeItem('token');
    this.currentUserSubject.next(null);
    this.router.navigate(['/products']);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    if (!token) return false;
    
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.exp > Date.now() / 1000;
    } catch {
      return false;
    }
  }

  isAdmin(): boolean {
    const user = this.currentUserSubject.value;
    return user?.roles.includes('Admin') || false;
  }

  private setToken(token: string): void {
    localStorage.setItem('token', token);
  }

  private loadUserFromToken(): void {
    const token = this.getToken();
    if (!token) return;

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      if (payload.exp > Date.now() / 1000) {
        const user: User = {
          username: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
          email: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
          roles: payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || []
        };
        this.currentUserSubject.next(user);
      }
    } catch (error) {
      console.error('Error parsing token:', error);
      this.logout();
    }
  }
}