export class SessionService {
  private static tokenKey = 'gestran_token';

  static setToken(token: string) {
    localStorage.setItem(SessionService.tokenKey, token);
  }

  static getToken(): string | null {
    return localStorage.getItem(SessionService.tokenKey);
  }

  static clear() {
    localStorage.removeItem(SessionService.tokenKey);
  }

  static isLoggedIn(): boolean {
    return !!SessionService.getToken();
  }
}