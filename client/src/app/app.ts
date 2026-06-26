import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';


@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private http = inject(HttpClient);
  protected readonly title = signal('Dating App');
  protected members = signal<any[]>([]);

  async ngOnInit() {
    // Initialization logic can go here
    this.members.set(await this.getMembers());
  }

  async getMembers(): Promise<any[]> {
    try {
       return lastValueFrom(this.http.get<any[]>('https://localhost:7090/api/members'));
    } catch (error) {
      console.error('API Error:', error);
      throw error; // Rethrow the error to be handled by the caller if needed
    }
  }
}

