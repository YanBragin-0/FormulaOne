import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { CarService } from '../../services/car-service';
import { RouterLink } from '@angular/router';
import { MatIcon } from "@angular/material/icon";

@Component({
  selector: 'app-car-component',
  imports: [MatCardModule, CommonModule],
  templateUrl: './car-component.html',
  styleUrl: './car-component.scss',
})
export class CarComponent implements OnInit {
  private apiService = inject(CarService);
  protected carsData = signal<any[]>([])
  protected columns = ['title','description','team'];
  ngOnInit(): void {
    console.log('Fetching cars data...');
    this.apiService.getAllCars().subscribe({
      next: (data: any) => {
        this.carsData.set(data);
        console.log('Cars data received:', data);
      },
      error: (error: any) => console.error('Error fetching cars data:', error)
    });
  }
}
