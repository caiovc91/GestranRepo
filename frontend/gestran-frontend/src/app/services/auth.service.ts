import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { SessionService } from './session.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    constructor(private api: ApiService, private session: SessionService) {}

    async login(username: string, password: string) {
        const res = await this.api.post<{ token: string }>('/auth/login', { username, password });
        this.session.setToken(res.token);
        return res.token;
    }

    logout() {
        this.session.clearSession();
    }
}