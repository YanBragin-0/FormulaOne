import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { TeamService } from '../../services/team-service';
import { RouterLink } from '@angular/router';
import { MatIconModule } from "@angular/material/icon";
import { first } from 'rxjs';

@Component({
  selector: 'app-team-list-component',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatCardModule, RouterLink, MatIconModule],
  templateUrl: './team-list-component.html',
  styleUrl: './team-list-component.scss',
})
export class TeamListComponent implements OnInit {
    private apiService = inject(TeamService);
    protected teamsData = signal<any[]>([]);
    protected columns = ['teamName','biography'];
    ngOnInit(): void {
      console.log('TeamList component initialized. Fetching data...');
      this.apiService.getAllTeam().pipe(first()).subscribe({
        next: (data: any) => {
          this.teamsData.set(data);
          console.log('Teams data received:', data);
          data.forEach((t: { teamName: string; }) => console.log('Ожидаемый файл:', `assets/images/Logos/${t.teamName.trim()}.png`));
        },
        error: (error: any) => console.error('Error fetching teams data:', error)
      });
    }
}