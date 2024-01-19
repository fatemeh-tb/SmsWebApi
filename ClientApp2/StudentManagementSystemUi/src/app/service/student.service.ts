import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { CourseStudent } from '../domain/coursestudent.model';
import { Student } from '../domain/student.model';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  constructor(private http: HttpClient) {}

  private baseUrl = 'https://localhost:7291/';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  public getStudentsLit(): Observable<Student[]> {
    return this.http.get<Student[]>(this.baseUrl + 'students');
  }

  getStudentById(id: number): Observable<any> {
    return this.http.get(this.baseUrl + 'Student' + '/' + id);
  }

  addStudent(student: Student): Observable<any> {
    return this.http.post<Student>(
      this.baseUrl + 'Student',
      student
    )
  }

  addNewCourse(courseStudent: CourseStudent) {
    return this.http.post<Student>(
      this.baseUrl + 'CourseStudent',
      courseStudent,
      this.httpOptions
    );
  }

  removeStudent(id: number) {
    return this.http.delete(this.baseUrl + 'deleteStudent' + '/' + id);
  }

  updateStudent(student: Student): Observable<Student> {
    return this.http.put<Student>(this.baseUrl + 'Student', student);
  }
}
