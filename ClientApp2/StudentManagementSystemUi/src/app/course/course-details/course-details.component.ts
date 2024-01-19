import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Course } from 'src/app/domain/course.model';
import { Score } from 'src/app/domain/score.model';
import { CourseService } from 'src/app/service/course.service';
import { DataShareService } from 'src/app/service/dataShare.service';
import { ScoreService } from 'src/app/service/score.service';

@Component({
  selector: 'app-course-details',
  templateUrl: './course-details.component.html',
  styleUrls: ['./course-details.component.css'],
})
export class CourseDetailsComponent implements OnInit {
  courseDetails: Course[] = [];
  courseId: number;
  registeredCourse = [];
  formGroup: FormGroup;
  studentId: number;

  constructor(
    private courseService: CourseService,
    public dataShare: DataShareService,
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.dataShare.formGroup = new FormGroup({
      id: new FormControl(''),
      title: new FormControl('', Validators.required),
      courseCode: new FormControl('', [
        Validators.required,
        Validators.pattern('^[0-9]*$'),
      ]),
      students: new FormControl(''),
    });
  }

  ngOnInit(): void {
    this.showRegisteredCourses();
    this.route.params.subscribe((params) => {
      this.courseId = params['id'];
    });

    this.courseService.getCourseById(this.courseId).subscribe((result) => {
      this.courseDetails.push(result);
      this.courseDetails.forEach((element) => {
        this.dataShare.formGroup.setValue({
          id: element.id,
          title: element.title,
          courseCode: element.courseCode,
          students: '',
        });
      });
      this.showRegisteredCourses();
    });

    this.formGroup = new FormGroup({
      score1: new FormControl('', [Validators.required]),
      score2: new FormControl('', [Validators.required]),
      score3: new FormControl('', [Validators.required]),
      score4: new FormControl('', [Validators.required]),
      finalScore: new FormControl('', [Validators.required]),
    });
  }

  public showRegisteredCourses() {
    this.registeredCourse = [];
    this.courseDetails.forEach((item) => {
      var courses = item.courseStudent;
      courses.forEach((i) => {
        this.registeredCourse.push(i.student);
      });
    });
  }

  public studentScores(courseId: number, studentId: number) {
    this.router.navigate(['score', courseId, studentId]);
  }

  public removeHandler(studentId: number, courseId: number) {
    if (confirm('مطمئنید میخواهید حدف کنید ؟')) {
      this.courseService
        .removeCourseStudent(studentId, courseId)
        .subscribe((res) => {
          this.showRegisteredCourses();
          this.snackBar.open('', '! با موفقیت حذف شد ', {
            duration: 5000,
            verticalPosition: 'top',
          });
        });
    }
    window.location.reload();
  }

  public goToStudentDetails(id: number, fName: string, lName: string) {
    this.router.navigate(['studentslist', id, fName + '%' + lName]);
  }
}
