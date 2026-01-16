import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequest,RegisterRequest } from '../models/auth.model';
import { getAuth, onAuthStateChanged, User } from 'firebase/auth';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/auth';
  constructor(private http: HttpClient) {}

  login(data: LoginRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/Login`, data, {
      withCredentials: true 
    });
    
  }
  register(data: RegisterRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/registration`, data, {
      withCredentials: true 
    });
  }
}
