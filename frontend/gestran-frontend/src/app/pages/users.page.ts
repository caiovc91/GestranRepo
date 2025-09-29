import { ApiService } from '../services/api.service';
import { SessionService } from '../services/session.service';

export class UsersPage {
  static async render(root: HTMLElement) {
    if (!SessionService.isLoggedIn()) { window.location.href = '/login'; return; }

    root.innerHTML = '<h2>Users</h2><table class="table" id="users-table"><thead><tr><th>Name</th><th>Role</th><th>Active</th></tr></thead><tbody></tbody></table>';
    try {
      const controller = new AbortController();
      const users = await ApiService.getUsers(controller.signal);
      const tbody = document.querySelector('#users-table tbody')!;
      users.forEach((u: any) => {
        const tr = document.createElement('tr');
        tr.innerHTML = `<td>${u.name}</td><td>${u.role}</td><td>${u.isAccessActive}</td>`;
        tbody.appendChild(tr);
      });
    } catch {
      root.innerHTML += `<p class="text-danger">Failed to load users.</p>`;
    }
  }
}