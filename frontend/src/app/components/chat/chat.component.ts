import { Component } from '@angular/core';
import { ChatService } from '../../services/chat/chat.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-chat',
  imports: [CommonModule, FormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule],
  standalone: true,
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
  userMessage: string = '';
  chatHistory: { userMessage: string, chatGPTResponse: string }[] = [];
  userId: string | null = '';

  constructor(private chatService: ChatService, private authService: AuthService) {}

  ngOnInit() {
    this.userId = this.authService.getUserId();
    if (this.userId) {
      this.loadMessages();
    } else {
      console.error('Erro: Usuário não autenticado.');
    }
  }

  loadMessages() {
    this.chatService.getMessagesByUser(this.userId!).subscribe(
      (messages) => {
        this.chatHistory = messages.reverse();
      },
      (error) => {
        console.error('Erro ao carregar mensagens:', error);
      }
    );
  }

  sendMessage() {
    if (!this.userMessage.trim()) return;

    const userMessage = this.userMessage;
    this.chatHistory.push({ userMessage: userMessage, chatGPTResponse: 'Pensando...' });

    this.chatService.sendMessage(userMessage, this.authService.getUserId()).subscribe(response => {
      this.chatHistory[this.chatHistory.length - 1].chatGPTResponse = response.chatGPTResponse;
    });

    this.userMessage = '';
  }
}
