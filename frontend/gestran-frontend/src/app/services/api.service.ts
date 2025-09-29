import { SessionService } from './session.service';

const API_BASE = 'https://localhost:7194/api';

export class ApiService {
  private static request(url: string, options: any = {}, signal?: AbortSignal) {
    const token = SessionService.getToken();
    const headers = { 'Content-Type': 'application/json', ...(options.headers || {}) };
    if (token) headers['Authorization'] = `Bearer ${token}`;

    return fetch(`${API_BASE}${url}`, { ...options, headers, signal })
      .then(res => {
        if (!res.ok) throw new Error(`HTTP error! status: ${res.status}`);
        return res.json();
      });
  }

  static login(name: string, password: string) {
    return this.request('/auth/login', { method: 'POST', body: JSON.stringify({ name, password }) });
  }

  static getUsers(signal?: AbortSignal) { return this.request('/users', { method: 'GET' }, signal); }
  static getCheckLists(signal?: AbortSignal) { return this.request('/checklist', { method: 'GET' }, signal); }
  static createCheckList(data: any, signal?: AbortSignal) { return this.request('/checklist', { method: 'POST', body: JSON.stringify(data) }, signal); }
  static updateCheckList(id: string, data: any, signal?: AbortSignal) { return this.request(`/checklist/${id}`, { method: 'PUT', body: JSON.stringify(data) }, signal); }
  static deleteCheckList(id: string, signal?: AbortSignal) { return this.request(`/checklist/${id}`, { method: 'DELETE' }, signal); }

  static getCheckListItemTypes(signal?: AbortSignal) { return this.request('/checklistitemtype', { method: 'GET' }, signal); }
  static createCheckListItemType(data: any, signal?: AbortSignal) { return this.request('/checklistitemtype', { method: 'POST', body: JSON.stringify(data) }, signal); }
  static updateCheckListItemType(id: string, data: any, signal?: AbortSignal) { return this.request(`/checklistitemtype/${id}`, { method: 'PUT', body: JSON.stringify(data) }, signal); }
  static deleteCheckListItemType(id: string, signal?: AbortSignal) { return this.request(`/checklistitemtype/${id}`, { method: 'DELETE' }, signal); }
}