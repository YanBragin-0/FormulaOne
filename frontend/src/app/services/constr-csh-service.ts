import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class ConstrCshService {
  
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5000/api/ConstructorsChampionshipRead/GetComstructorsTable';
  getData(year: number = 2025): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}?year=${year}`);
  } 
}
