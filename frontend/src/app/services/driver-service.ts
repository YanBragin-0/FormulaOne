import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class DriverService {
  private http = inject(HttpClient);
  private apiUrlGetAllDrivers = 'http://localhost:5000/api/DriversRead/GetAllDrivers';
  private apiUrlGetByTeam = 'http://localhost:5000/api/DrivesrRead/GetTeamDrivers';
  getAllDrivers(): Observable<any> {
    return this.http.get<any>(this.apiUrlGetAllDrivers,{withCredentials: true});
  }
  getDriversByTeam(teamName: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrlGetByTeam}?teamName=${teamName}`,{withCredentials: true});
  }
}
