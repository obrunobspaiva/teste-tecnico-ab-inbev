import { Component } from '@angular/core';
import { ChatService } from '../../services/chat/chat.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-chat',
  imports: [FormsModule],
  standalone: true,
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
  userMessage: string = '';
  chatHistory: { user: string, bot: string }[] = [];

  constructor(private chatService: ChatService) {}

  sendMessage() {
    if (!this.userMessage.trim()) return;

    const userMessage = this.userMessage;
    this.chatHistory.push({ user: userMessage, bot: 'Pensando...' });

    this.chatService.sendMessage(userMessage).subscribe(response => {
      this.chatHistory[this.chatHistory.length - 1].bot = response.chatGPTResponse;
    });

    this.userMessage = '';
  }
}
