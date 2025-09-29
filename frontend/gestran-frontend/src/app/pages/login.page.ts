import { ApiService } from '../services/api.service';
import { SessionService } from '../services/session.service';
import { Router } from '../router';

export class LoginPage {
  static render(root: HTMLElement) {
    root.innerHTML = `
      <h2>Login</h2>
      <div class="mb-3"><label>Name</label><input type="text" class="form-control" id="login-name"></div>
      <div class="mb-3"><label>Password</label><input type="password" class="form-control" id="login-password"></div>
      <button class="btn btn-primary" id="login-btn">Login</button>
      <p class="text-danger mt-2" id="login-error"></p>
    `;
    document.getElementById('login-btn')!.onclick = async () => {
      const name = (document.getElementById('login-name') as HTMLInputElement).value;
      const password = (document.getElementById('login-password') as HTMLInputElement).value;
      try {
        const data = await ApiService.login(name, password);
        SessionService.setToken(data.token);
        window.history.pushState({}, '', '/users');
        Router.load('/users');
      } catch {
        document.getElementById('login-error')!.innerText = 'Login failed';
      }
    };
  }
}