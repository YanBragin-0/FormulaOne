import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { DriverService } from '../../services/driver-service';
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-driver-component',
  imports: [CommonModule, MatCardModule],
  templateUrl: './driver-component.html',
  styleUrl: './driver-component.scss',
})
export class DriverComponent implements OnInit {
  private apiService = inject(DriverService);
  protected driversData = signal<any[]>([]);
  protected columns = ['name','age','country','team','biography'];
  ngOnInit(): void {
    console.log('Fetching drivers data...');
    this.apiService.getAllDrivers().subscribe({
      next: (data: any) => {
        this.driversData.set(data);
        console.log('Drivers data received:', data);
      },
      error: (error: any) => console.error('Error fetching drivers data:', error)
    });
  }
  getDriversByTeam(teamName: string): void {
    console.log(`Fetching drivers for team: ${teamName}...`); 
    this.apiService.getDriversByTeam(teamName).subscribe({
      next: (data: any) => {
        this.driversData.set(data);
        console.log(`Drivers data for team ${teamName} received:`, data);
      },
      error: (error: any) => console.error(`Error fetching drivers for team ${teamName}:`, error)
    });
  }
}
