Arquivo: app/components/header.component.ts

import { Component } from '@angular/core';
import { SessionService } from '../services/session.service';

@Component({
  selector: 'app-header',
  template: `
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <a class="navbar-brand" href="#">Gestran</a>
    <div class="collapse navbar-collapse">
      <ul class="navbar-nav me-auto">
        <li class="nav-item" *ngIf="session.isOperator()">
          <a class="nav-link" routerLink="/execute-checklist">Execute Checklist</a>
        </li>
        <li class="nav-item" *ngIf="session.isSupervisor()">
          <a class="nav-link" routerLink="/approval">Approval</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" (click)="logout()">Logout</a>
        </li>
      </ul>
    </div>
  </nav>
  `
})
export class HeaderComponent {
  constructor(public session: SessionService) {}
  logout() { this.session.logout(); window.location.href='/login'; }
}