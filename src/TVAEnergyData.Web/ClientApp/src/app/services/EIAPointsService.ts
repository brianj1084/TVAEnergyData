import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { EIAPoint } from '../models/EIAPoint';

@Injectable()
export class EIAPointsService {
  headers = new HttpHeaders().set('Content-Type', 'application/json');

  constructor(private http: HttpClient) {

  }

  getAllPoints(): Observable<EIAPoint[]> {
    return this.http.get<EIAPoint[]>('/api/EIAPoints');
  }

  getPointById(id: number): Observable<EIAPoint> {
    return this.http.get<EIAPoint>(`/api/EIAPoints/${id}`);
  }

  updatePoint(point: EIAPoint) {
    return this.http.put(`/api/EIAPoints/${point.id}`, point, { headers: this.headers });
  }

  createPoint(point: EIAPoint): Observable<EIAPoint> {
    return this.http.post<EIAPoint>('/api/EIAPoints', point, { headers: this.headers });
  }

  deletePoint(id: number) {
    return this.http.delete(`/api/EIAPoints/${id}`);
  }
}
