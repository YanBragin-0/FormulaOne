import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RaceService {
  private http = inject(HttpClient);
  private apiUrlAllRaces = 'http://localhost:5000/api/RaceRead/GetAllRaces';
  getAllRaces(): Observable<any> {
    return this.http.get<any>(this.apiUrlAllRaces,{withCredentials: true});
  }
  getRaceByCountry(country: string): Observable<any> {
    return this.http.get<any>(`http://localhost:5000/api/RaceRead/GetRaceByCountry?country=${country}`,{withCredentials: true});
  }
}
