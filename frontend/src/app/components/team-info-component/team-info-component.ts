import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { TeamService } from '../../services/team-service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MatIconModule } from "@angular/material/icon";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";

@Component({
  selector: 'app-team-info-component',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatCardModule, RouterLink, MatIconModule,MatProgressSpinnerModule],
  templateUrl: './team-info-component.html',
  styleUrl: './team-info-component.scss',
})
export class TeamInfoComponent implements OnInit {
    private route = inject(ActivatedRoute);
    private apiService = inject(TeamService);
    protected teamInfo = signal<any>(null);
    ngOnInit(): void {
      console.log('TeamInfo component initialized. Fetching data...');
      const teamName = this.route.snapshot.paramMap.get('teamName');
      if (teamName) {
        this.apiService.getTeamByName(teamName).subscribe(
        {
          next: (data: any) => this.teamInfo.set(data),
          error: (error: any) => console.error('Error fetching team data:', error)
        }
      );  
    }
  }
}
