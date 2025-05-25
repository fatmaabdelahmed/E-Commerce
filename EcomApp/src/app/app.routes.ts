// app.routes.ts
import { Routes } from '@angular/router';
import { AUTH_ROUTES } from './auth/auth.routes';
import { ProductListComponent } from './products/product-list.component';

export const routes: Routes = [
  ...AUTH_ROUTES,
  // { path: '', redirectTo: 'login', pathMatch: 'full' },  
  // { path: '**', redirectTo: 'login' }, 
   { path: '', component: ProductListComponent },

  { path: '**', redirectTo: '' }                  
];