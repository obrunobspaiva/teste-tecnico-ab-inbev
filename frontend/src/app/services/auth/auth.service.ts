import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'token';
  private apiUrl = 'http://localhost:5086/api/auth/login';

  constructor(private http: HttpClient, @Inject(PLATFORM_ID) private platformId: Object) { }

  login(username: string, password: string): Observable<any> {
    return this.http.post<any>(this.apiUrl, { username: username, passwordHash: password });
  }

  setToken(token: string): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.setItem(this.tokenKey, token);
    }
  }

  getToken(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem(this.tokenKey);
    }
    return null;
  }

  isAuthenticated(): boolean {
    return isPlatformBrowser(this.platformId) && !!this.getToken();
  }

  logout(): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem(this.tokenKey);
    }
  }

  getUserId(): string | null {
    const token = this.getToken();
    if (!token) {
      console.error("Erro: Nenhum token encontrado.");
      return null;
    }

    try {
      const tokenPayloadBase64 = token.split('.')[1];
      const tokenPayloadDecoded = JSON.parse(atob(tokenPayloadBase64));

      return tokenPayloadDecoded.userId || null;
    } catch (error) {
      console.error('Erro ao decodificar o token:', error);
      return null;
    }
  }


}
