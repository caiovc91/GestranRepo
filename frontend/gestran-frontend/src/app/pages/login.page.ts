import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SessionService } from '../services/session.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html'
})
export class LoginPage {
  username = '';
  password = '';

  constructor(private session: SessionService, private router: Router) {}

  async submitLogin(event: Event) {
    event.preventDefault();
    try {
      const user = await this.session.login(this.username, this.password);
      if (user.Role === 'Operator') this.router.navigate(['/execute-checklist']);
      else if (user.Role === 'Supervisor') this.router.navigate(['/approval']);
    } catch (err) {
      alert('Login failed');
    }
  }
}