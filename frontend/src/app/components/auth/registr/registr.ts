import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth-service';
import { Router } from '@angular/router';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-registr',
  imports: [FormsModule],
  templateUrl: './registr.html',
  styleUrl: './registr.scss',
})
export class Registr {
  Login ='';
  Email = '';
  Password = '';

  isLogin = false;
  constructor(private authService: AuthService, private router: Router) {}
  onRegister() {
    this.authService.register({Login: this.Login, Email: this.Email, Password: this.Password }).subscribe({
      next: () => {
        console.log('Успешная регистрация!');
        this.router.navigate(['/login']); 
      },
      error: (err) => alert('Ошибка регистрации: ' + err.status)
    });
  }
  loginWithGoogle() {
    window.location.href = 'http://localhost:5000/auth/loginWith/google';
  }
  onSubmit(){
    this.onRegister();
  }
}
