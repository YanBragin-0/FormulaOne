import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class TeamService {
  private http = inject(HttpClient);
  private apiUrlAllTeams = 'http://localhost:5000/api/TeamRead/GetAllTeams';
  private apiUrlTeamByName = 'http://localhost:5000/api/TeamRead/GetTeamByName';
  getAllTeam(): Observable<any> {
    return this.http.get<any>(this.apiUrlAllTeams,{withCredentials: true});
  }
  getTeamByName(teamName: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrlTeamByName}?teamName=${teamName}`,{withCredentials: true});
  }
}
