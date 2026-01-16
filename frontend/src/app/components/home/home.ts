import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field'; 
import { MatInputModule } from '@angular/material/input'; 
import { HttpClient } from '@angular/common/http';

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [CommonModule,
    MatCardModule,
    MatIconModule,
    RouterLink,
    MatFormFieldModule,
    MatInputModule],
  templateUrl: './home.html',
  styleUrls: ['./home.scss'],
})
export class Home {
  private router = inject(Router);
  private http = inject(HttpClient);
  elasticResults = signal<any[]>([]);
  private readonly Search_map : Record<string, string> = {
    'team': '/allTeams',
    'driver': '/drivers',
    'car': '/cars',
    'race': '/races',
    'circuit': '/circuits',
    'driversChampionship 2025': '/driversTable',
    'constructorsChampionship 2025': '/constructorsTable'
  };
  onSearch(query: string) {
    if (!query.trim()) {
      this.elasticResults.set([]);
      return;
    }
    this.http.get<any[]>(`http://localhost:5000/api/Search/Search?queryString=${query}`)
      .subscribe({
        next: (data) => {
          this.elasticResults.set(data);
        },
        error: (err) => {
          console.error('Ошибка поиска:', err);
          this.elasticResults.set([]);
        }
      });
  }
  onResultClick(item: any) {
    
      const baseRoute = this.Search_map[item.type];
      if (baseRoute) {
        this.router.navigate([baseRoute]);
      }
      else {
        console.warn(`No route defined for type: ${item}`);
      }
  }
  
}
