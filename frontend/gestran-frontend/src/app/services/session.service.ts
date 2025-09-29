import { ApiService } from './api.service';
import { User } from '../models/user.model';

export class SessionService {
  private currentUser: User | null = null;

  constructor(private api: ApiService) {}

  async login(username: string, password: string) {
    const user = await this.api.post<User>('/Auth/Login', { username, password });
    this.currentUser = user;
    localStorage.setItem('sessionUser', JSON.stringify(user));
    return user;
  }

  logout() {
    this.currentUser = null;
    localStorage.removeItem('sessionUser');
  }

  getUser(): User | null {
    if (!this.currentUser) {
      const stored = localStorage.getItem('sessionUser');
      if (stored) this.currentUser = JSON.parse(stored);
    }
    return this.currentUser;
  }

  isOperator() { return this.getUser()?.role === 'Operator'; }
  isSupervisor() { return this.getUser()?.role === 'Supervisor'; }
}