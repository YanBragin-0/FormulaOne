import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { CircuitService } from '../../services/circuit-service';
import { RouterLink } from '@angular/router';
import { MatIcon } from "@angular/material/icon";

@Component({
  selector: 'app-circuit-component',
  imports: [MatCardModule, CommonModule, MatIcon],
  templateUrl: './circuit-component.html',
  styleUrl: './circuit-component.scss',
})
export class CircuitComponent implements OnInit {
  private apiService = inject(CircuitService);
  protected circuitsData = signal<any[]>([]);
  protected columns = ['title','country','length'];
  ngOnInit(): void {
    console.log('Fetching circuits data...');
    this.apiService.getAllCircuits().subscribe({
      next: (data: any) => {
        this.circuitsData.set(data);
        console.log('Circuits data received:', data);
      },
      error: (error: any) => console.error('Error fetching circuits data:', error)
    });
  }
}
