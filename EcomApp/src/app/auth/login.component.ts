import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'] // أو scss لو بتحبي
})
export class LoginComponent {
  email = '';
  password = '';
  error = '';
  successMessage = '';
  isLoading = false;

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.error = '';
    this.successMessage = '';
    this.isLoading = true;

    const data = {
      username: this.email,
      password: this.password
    };

    this.authService.login(data).subscribe({
      next: (res: any) => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('username', res.username);
        this.successMessage = 'Login successful! Redirecting...';
        this.isLoading = false;

        setTimeout(() => {
          this.router.navigate(['/']);
        }, 2000);
      },
      error: (err: any) => {
        this.error = err.error?.message || 'Invalid credentials';
        this.isLoading = false;
      }
    });
  }
}
