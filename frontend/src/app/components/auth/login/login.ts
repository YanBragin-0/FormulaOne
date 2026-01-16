import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth-service';
import { Router } from '@angular/router';
import {FormsModule} from '@angular/forms';
@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  Login ='';
  Email = '';
  Password = '';

  isLogin = true;


  constructor(private authService: AuthService, private router: Router) {}
  onLogin() {
    this.authService.login({Login: this.Login, Email: this.Email, Password: this.Password }).subscribe({
      next: () => {
        console.log('Успешный вход!');
        this.router.navigate(['']); 
      },
      error: (err) => alert('Ошибка входа: ' + err.status)
    });
  }
  loginWithGoogle() {
    try
    {
      window.location.href = 'http://localhost:5000/auth/loginWith/google';
    } 
    catch (error) 
    {
      console.error('Error during Google login redirect:', error);
    }
  }
  onSubmit(){
    this.onLogin();
  }
}
