import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Course } from '../domain/course.model';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  private baseUrl = 'https://localhost:7291/';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  }

  constructor(private http: HttpClient) { }

  public getCoursesList(): Observable<Course[]> {
    return this.http.get<Course[]>(this.baseUrl + 'courses');
  }

  getCourseById(id: number): Observable<any> {
    return this.http.get(this.baseUrl + 'Course' + '/' + id);
  }

  addCourse(course: Course){
    return this.http.post<Course>(this.baseUrl + 'Course', course, this.httpOptions);
  }

  removeCourse(id: number) {
    return this.http.delete(this.baseUrl + 'deleteCourse' + '/' + id);
  }

  removeCourseStudent(Sid: number, Cid: number) {
    return this.http.delete(this.baseUrl + 'deleteCourseStudent' + '/' + Sid + '/' + Cid);
  }
}
