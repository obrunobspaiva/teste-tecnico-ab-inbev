import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-register',
  imports: [CommonModule, FormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule, MatIconModule],
  standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  username = '';
  password = '';
  errorMessage = '';

  constructor(private userService: UserService, private router: Router) {}

  register() {
    this.userService.register(this.username, this.password).subscribe(
      () => {
        alert('Cadastro realizado com sucesso!');
        this.router.navigate(['/login']);
      },
      (error) => {
        this.errorMessage = 'Erro ao cadastrar usuário. Tente novamente.';
      }
    );
  }
}
