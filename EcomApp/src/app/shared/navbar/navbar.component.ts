// import { Component } from '@angular/core';
// import { Router } from '@angular/router';

// @Component({
//   selector: 'app-navbar',
//   standalone: true,
//   imports: [CommonModule, RouterModule],
//   templateUrl: './navbar.component.html',
//   styleUrls: ['./navbar.component.scss']
// })

// export class NavbarComponent {
//   username: string | null = null;

//   constructor(private router: Router) {
//     this.username = localStorage.getItem('username');
//   }

//   isLoggedIn(): boolean {
//     return !!localStorage.getItem('token');
//   }

//   logout() {
//     localStorage.removeItem('token');
//     localStorage.removeItem('username');
//     this.username = null;
//     this.router.navigate(['/auth/login']);
//   }
// }
