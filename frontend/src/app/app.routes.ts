import { Routes } from '@angular/router';
import { Home } from './components/home/home';
import { ConstructorsTable } from './components/constructors-table/constructors-table';
import { DriversTable } from './components/drivers-table/drivers-table';
import { TeamListComponent } from './components/team-list-component/team-list-component';
import { TeamInfoComponent } from './components/team-info-component/team-info-component';       
import { Login } from './components/auth/login/login';
import { Registr } from './components/auth/registr/registr';
import { RaceComponent } from './components/race-component/race-component';
import { DriverComponent } from './components/driver-component/driver-component';
import { CircuitComponent } from './components/circuit-component/circuit-component';
import { CarComponent } from './components/car-component/car-component';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'constructorsTable', component: ConstructorsTable },
  { path: 'driversTable', component: DriversTable },
  { path: 'allTeams',component: TeamListComponent},
  { path: 'teamInfo/:teamName',component: TeamInfoComponent},
  { path: 'login', component: Login },
  { path: 'registr', component: Registr },
  { path: 'races', component: RaceComponent },
  { path: 'drivers', component: DriverComponent },
  { path: 'circuits', component: CircuitComponent },
  { path: 'cars', component: CarComponent },
];