import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { Course } from '../domain/course.model';
import { CourseService } from '../service/course.service';
import { DataShareService } from '../service/dataShare.service';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css'],
})
export class CourseComponent implements OnInit {
  course: Course[];
  isLoading: boolean = false;
  registeredCourse = [];

  displayedColumns: string[] = ['Index', 'title', 'courseCode'];
  public dataSource: MatTableDataSource<Course>;

  constructor(
    private courseService: CourseService,
    public dataShare: DataShareService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getCourses();
  }


  public getCourses() {
    this.isLoading = true;
    this.courseService.getCoursesList().subscribe(
      (res) => {
        this.isLoading = false;
        this.course = res;
        this.dataSource = new MatTableDataSource<Course>(this.course);
      },
      (err: HttpErrorResponse) => {
        console.log(err);
      }
    );
  }

  public removeHandler(id: number) {
    if (confirm('مطمئنید میخواهید حدف کنید ؟')) {
      this.courseService.removeCourse(id).subscribe((res) => {
        this.getCourses();
        this.snackBar.open('', '! با موفقیت حذف شد ', {
          duration: 5000,
          verticalPosition: 'top',
        });
      });
    } else {
      this.getCourses();
    }
  }
}
