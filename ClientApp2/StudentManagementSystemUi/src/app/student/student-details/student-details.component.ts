import { isNull } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { element } from 'protractor';
import { Course } from 'src/app/domain/course.model';
import { CourseStudent } from 'src/app/domain/coursestudent.model';
import { Student } from 'src/app/domain/student.model';
import { CourseService } from 'src/app/service/course.service';
import { DataShareService } from 'src/app/service/dataShare.service';
import { StudentService } from 'src/app/service/student.service';

@Component({
  selector: 'app-student-details',
  templateUrl: './student-details.component.html',
  styleUrls: ['./student-details.component.css'],
})
export class StudentDetailsComponent implements OnInit {
  studentDetails: Student[] = [];
  registeredCourse = [];
  course: Course[];
  isCourseExist: boolean = false;
  studentId: number;

  constructor(
    private studentService: StudentService,
    private courseService: CourseService,
    public dataShare: DataShareService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar,
    private router: Router
  ) {
    this.dataShare.formGroup = new FormGroup({
      id: new FormControl(''),
      fName: new FormControl('', Validators.required),
      lName: new FormControl('', Validators.required),
      phone: new FormControl('', [
        Validators.required,
        Validators.pattern('^[0-9]*$'),
      ]),
      nationalCode: new FormControl('', [
        Validators.required,
        Validators.pattern('^[0-9]*$'),
        Validators.minLength(10),
      ]),
      parentName: new FormControl('', Validators.required),
      gender: new FormControl('', Validators.required),
      imagePath: new FormControl(''),
      courses: new FormControl(''),
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.studentId = params['id'];
    });

    this.studentService.getStudentById(this.studentId).subscribe((result) => {
      this.studentDetails.push(result);
      this.studentDetails.forEach((element) => {
        this.dataShare.formGroup.setValue({
          id: element.id,
          fName: element.fName,
          lName: element.lName,
          phone: element.phone,
          parentName: element.parentName,
          nationalCode: element.nationalCode,
          gender: element.gender,
          imagePath: element.imagePath,
          courses: '',
        });
      });
    });

    this.courseService.getCoursesList().subscribe((res) => {
      this.course = res;
    });
  }

  public showRegisteredCourses() {
    this.registeredCourse = [];
    this.studentDetails.forEach((item) => {
      var courses = item.courseStudent;
      courses.forEach((i) => {
        this.registeredCourse.push(i.course);
      });
    });
  }

  addNewCourse(id: number, selected: number) {
    const newCourse: CourseStudent = {
      studentsId: id,
      coursesId: selected,
      student: null,
      course: null,
    };
    const courseExist = this.registeredCourse.find(
      (x) => x.id == newCourse.coursesId
    );

    if (courseExist) {
      this.isCourseExist = true;
    } else {
      this.studentService.addNewCourse(newCourse).subscribe((res) => {
        window.location.reload();
      });
    }
  }

  onSubmit(studentFormValue) {
    const formValues = { ...studentFormValue };

    const student: Student = {
      id: formValues.id,
      fName: formValues.fName,
      lName: formValues.lName,
      parentName: formValues.parentName,
      nationalCode: formValues.nationalCode,
      phone: formValues.phone,
      gender: formValues.gender,
      imagePath: formValues.imagePath,
      scores: [],
      courseStudent: [],
    };
    this.studentService.updateStudent(student).subscribe((res) => {
      this.router.navigate(['studentslist']).then((navigated: boolean) => {
        if (navigated) {
          this.snackBar.open('', '! ویرایش اطلاعات با موفقیت انجام شد', {
            duration: 900,
            verticalPosition: 'top',
          });
        }
      });
    });
  }
}
