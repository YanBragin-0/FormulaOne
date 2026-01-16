import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table'; 
import { MatCardModule } from '@angular/material/card';   
import { DriversCshService } from '../../services/drivers-csh-service';

@Component({
  selector: 'app-drivers-table',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatCardModule],
  templateUrl: './drivers-table.html',
  styleUrls: ['./drivers-table.scss'],
})
export class DriversTable implements OnInit {
  private apiService = inject(DriversCshService);
  protected driversData = signal<any[]>([]);
  protected columns = ['position','Name','TeamName','Points'];
  ngOnInit(): void {
    console.log('Drivers component initialized. Fetching data...');
    this.apiService.getData(2025).subscribe({
      next: (data:any) => {this.driversData.set(data);
      console.log('Constructors data received:', data);
      },
      error: (err:any) => console.error(err)
    });
  }
}
