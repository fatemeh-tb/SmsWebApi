import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { StudentService } from './student.service';

@Injectable({
  providedIn: 'root',
})
export class DataShareService {
  formGroup: FormGroup;

  constructor(private studentService: StudentService) {}

  public validateControl = (controlName: string) => {
    return (
      this.formGroup.get(controlName).invalid &&
      this.formGroup.get(controlName).touched
    );
  };

  public hasError = (controlName: string, errorName: string) => {
    return this.formGroup.get(controlName).hasError(errorName);
  };
}
