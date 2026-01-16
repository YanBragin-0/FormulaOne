import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { ConstrCshService } from '../../services/constr-csh-service';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-constructors-table',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatCardModule],
  templateUrl: './constructors-table.html',
  styleUrls: ['./constructors-table.scss'],
})
export class ConstructorsTable implements OnInit {
  private apiService = inject(ConstrCshService);
  protected constructorsData = signal<any[]>([]);
  protected columns = ['position','Team','Points'];
  ngOnInit(): void {
    console.log('Constructors component initialized. Fetching data...');
    this.apiService.getData(2025).subscribe({
      next: (data: any) => {
        this.constructorsData.set(data);
        console.log('Constructors data received:', data);
      },
      error: (error: any) => console.error('Error fetching constructors data:', error)
    });
  }
}
