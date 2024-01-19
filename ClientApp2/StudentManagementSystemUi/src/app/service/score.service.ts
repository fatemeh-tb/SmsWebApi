import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Score } from '../domain/score.model';

@Injectable({
  providedIn: 'root',
})
export class ScoreService {
  constructor(private http: HttpClient) {}

  private baseUrl = 'https://localhost:7291/';

  addStudentScore(score: Score): Observable<any> {
    return this.http.post<Score>(this.baseUrl + 'Score', score);
  }

  public getStudentScore(): Observable<Score[]> {
    return this.http.get<Score[]>(this.baseUrl + 'scores');
  }

  getScoresById(id: number): Observable<any> {
    return this.http.get(this.baseUrl + 'scores' + '/' + id);
  }

  updateStudent(score: Score): Observable<Score> {
    return this.http.put<Score>(this.baseUrl + 'api/Score', score);
  }
}
