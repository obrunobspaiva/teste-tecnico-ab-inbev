import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private apiUrl = 'http://localhost:5086/api/chat';

  constructor(private http: HttpClient) {}

  sendMessage(userMessage: string): Observable<any> {
    return this.http.post<any>(this.apiUrl, { userMessage });
  }
}
