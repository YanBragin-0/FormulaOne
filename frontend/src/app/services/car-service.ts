import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CarService {
  private http = inject(HttpClient);
  private apiUrlAllCars = 'http://localhost:5000/api/CarsRead/GetAll';
  getAllCars(): Observable<any> {
    return this.http.get<any>(this.apiUrlAllCars,{withCredentials: true});
  }
}
