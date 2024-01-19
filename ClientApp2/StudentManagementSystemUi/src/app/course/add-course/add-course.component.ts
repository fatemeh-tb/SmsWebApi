import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Course } from 'src/app/domain/course.model';
import { CourseService } from 'src/app/service/course.service';
import { DataShareService } from 'src/app/service/dataShare.service';

@Component({
  selector: 'app-add-course',
  templateUrl: './add-course.component.html',
  styleUrls: ['./add-course.component.css'],
})
export class AddCourseComponent implements OnInit {
  constructor(
    private courseService: CourseService,
    public dataShare: DataShareService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.dataShare.formGroup = new FormGroup({
      title: new FormControl('', [Validators.required]),
      courseCode: new FormControl('', [
        Validators.required,
        Validators.pattern('^[0-9]*$'),
      ]),
    });
  }

  onSubmit(studentFormValue) {
    const formValues = { ...studentFormValue };

    const course: Course = {
      id: 0,
      title: formValues.title,
      courseCode: formValues.courseCode,
      courseStudent: [],
      scores: [],
    };

    this.courseService.addCourse(course).subscribe((res) => {
      this.router.navigate(['courseslist']).then((navigated: boolean) => {
        if (navigated) {
          this.snackBar.open('', '! دوره جدید با موفقیت اضافه شد', {
            duration: 3000,
            verticalPosition: 'bottom',
          });
        }
      });
    });
  }
}
