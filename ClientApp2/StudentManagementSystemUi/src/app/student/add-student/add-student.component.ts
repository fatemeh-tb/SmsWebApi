import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable, Subscriber } from 'rxjs';
import { Student } from 'src/app/domain/student.model';
import { DataShareService } from 'src/app/service/dataShare.service';
import { StudentService } from 'src/app/service/student.service';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css'],
})
export class AddStudentComponent implements OnInit {
  image: any
  constructor(
    private studentService: StudentService,
    public dataShare: DataShareService,
    private snackBar: MatSnackBar,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.dataShare.formGroup = new FormGroup({
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

  ngOnInit(): void {}

  onSubmit(studentFormValue) {
    const formValues = { ...studentFormValue };

    const student: Student = {
      id: 0,
      fName: formValues.fName,
      lName: formValues.lName,
      phone: formValues.phone,
      nationalCode: formValues.nationalCode,
      parentName: formValues.parentName,
      gender: formValues.gender,
      imagePath: this.image,
      scores: [],
      courseStudent: [],
    };

    this.studentService.addStudent(student).subscribe((res) => {
      this.router.navigate(['studentslist']).then((navigated: boolean) => {
        if (navigated) {
          this.snackBar.open('', '! دانشجو جدید با موفقیت اضافه شد', {
            duration: 5000,
            verticalPosition: 'bottom',
          });
        }
      });
    });
  }

  onChange($event: Event) {
    const target = $event.target as HTMLInputElement;
    const files: File = (target.files as FileList)[0];
    this.convertobase64(files)
  }

  convertobase64(file: File) {
    const observable = new Observable((subscriber: Subscriber<any>) => {
      this.readFile(file, subscriber)
    })

    observable.subscribe((d) => {
      this.image = d
    })
  }

  readFile(file: File, subscriber: Subscriber<any>) {
    const fileReader = new FileReader();
    fileReader.readAsDataURL(file)

    fileReader.onload = () => {
      subscriber.next(fileReader.result);

      subscriber.complete()
    }

    fileReader.onerror = () => {
      subscriber.error()
      subscriber.complete()
    }
  }
}
