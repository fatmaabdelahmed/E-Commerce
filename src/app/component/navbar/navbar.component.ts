import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService, User } from '../../services/auth.service';
import { CartService } from '../../services/cart.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark sticky-top">
      <div class="container">
        <!-- Brand -->
        <a class="navbar-brand fw-bold" routerLink="/products">
          <i class="fas fa-spray-can me-2"></i>
          Perfume Store
        </a>

        <!-- Mobile toggle -->
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
          <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarNav">
          <!-- Main Navigation -->
          <ul class="navbar-nav me-auto">
            <li class="nav-item">
              <a class="nav-link" routerLink="/products" routerLinkActive="active">
                <i class="fas fa-shopping-bag me-1"></i>
                Products
              </a>
            </li>
          </ul>

          <!-- Right side navigation -->
          <ul class="navbar-nav">
            <!-- Cart -->
            <li class="nav-item" *ngIf="currentUser">
              <a class="nav-link position-relative" routerLink="/cart">
                <i class="fas fa-shopping-cart"></i>
                <span class="badge bg-danger position-absolute top-0 start-100 translate-middle rounded-pill" 
                      *ngIf="cartItemsCount > 0">
                  {{ cartItemsCount }}
                </span>
              </a>
            </li>

            <!-- Admin Panel -->
            <li class="nav-item dropdown" *ngIf="isAdmin">
              <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown">
                <i class="fas fa-cog me-1"></i>
                Admin
              </a>
              <ul class="dropdown-menu">
                <li><a class="dropdown-item" routerLink="/admin">
                  <i class="fas fa-chart-bar me-2"></i>Dashboard
                </a></li>
                <li><a class="dropdown-item" routerLink="/admin/products">
                  <i class="fas fa-box me-2"></i>Products
                </a></li>
                <li><a class="dropdown-item" routerLink="/admin/categories">
                  <i class="fas fa-tags me-2"></i>Categories
                </a></li>
              </ul>
            </li>

            <!-- User Menu -->
            <li class="nav-item dropdown" *ngIf="currentUser; else authLinks">
              <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown">
                <i class="fas fa-user me-1"></i>
                {{ currentUser.username }}
              </a>
              <ul class="dropdown-menu">
                <li><h6 class="dropdown-header">{{ currentUser.email }}</h6></li>
                <li><hr class="dropdown-divider"></li>
                <li><a class="dropdown-item" (click)="logout()">
                  <i class="fas fa-sign-out-alt me-2"></i>Logout
                </a></li>
              </ul>
            </li>

            <!-- Auth Links -->
            <ng-template #authLinks>
              <li class="nav-item">
                <a class="nav-link" routerLink="/login">
                  <i class="fas fa-sign-in-alt me-1"></i>
                  Login
                </a>