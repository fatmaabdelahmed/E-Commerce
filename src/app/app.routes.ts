import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';
import { adminGuard } from './guards/admin.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  
  // Public routes
  {
    path: 'products',
    loadComponent: () => import('./pages/product-list/product-list.component').then(m => m.ProductListComponent)
  },
  {
    path: 'product/:id',
    loadComponent: () => import('./pages/product-detail/product-detail.component').then(m => m.ProductDetailComponent)
  },
  {
    path: 'login',
    loadComponent: () => import('./pages/auth/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'register',
    loadComponent: () => import('./pages/auth/register/register.component').then(m => m.RegisterComponent)
  },
  
  // Protected user routes
  {
    path: 'cart',
    loadComponent: () => import('./pages/cart/cart.component').then(m => m.CartComponent),
    canActivate: [authGuard]
  },
  {
    path: 'checkout',
    loadComponent: () => import('./pages/checkout/checkout.component').then(m => m.CheckoutComponent),
    canActivate: [authGuard]
  },
  
  // Admin routes
  {
    path: 'admin',
    canActivate: [adminGuard],
    children: [
      {
        path: '',
        loadComponent: () => import('./pages/admin/dashboard/dashboard.component').then(m => m.DashboardComponent)
      },
      {
        path: 'products',
        loadComponent: () => import('./pages/admin/product-management/product-management.component').then(m => m.ProductManagementComponent)
      },
      {
        path: 'categories',
        loadComponent: () => import('./pages/admin/category-management/category-management.component').then(m => m.CategoryManagementComponent)
      }
    ]
  },
  
  // 404 page
  {
    path: '**',
    loadComponent: () => import('./pages/not-found/not-found.component').then(m => m.NotFoundComponent)
  }
];