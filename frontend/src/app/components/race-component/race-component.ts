import { Component,inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table'; 
import { RouterLink } from '@angular/router';
import { RaceService } from '../../services/race-service';
import { MatCardModule } from "@angular/material/card";

@Component({
  selector: 'app-race-component',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatCardModule],
  templateUrl: './race-component.html',
  styleUrl: './race-component.scss',
})
export class RaceComponent implements OnInit {
  private apiService = inject(RaceService);
  protected raceData = signal<any[]>([]);
  protected columns = ['dateTime','title','country','length'];
  ngOnInit(): void {
    console.log('Fetching race data...');
    this.apiService.getAllRaces().subscribe({
      next: (data: any) => {
        this.raceData.set(data);
        console.log('Race data received:', data);
      },
      error: (error: any) => console.error('Error fetching race data:', error)
    });
  }
}
