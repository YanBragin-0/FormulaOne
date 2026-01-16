import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class DriversCshService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5000/api/DriverChampionshipRead/GetDriversTable';
  getData(year: number = 2025): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}?year=${year}`);
  } 
}
