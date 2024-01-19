import { HttpErrorResponse } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Student } from '../domain/student.model';
import { StudentService } from '../service/student.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css'],
})
export class StudentComponent implements OnInit {
  student: Student[];
  isLoading: boolean = false;

  displayedColumns: string[] = [
    'index',
    'fName',
    'lName',
    'nationalCode',
    'details',
    'delete',
  ];
  public dataSource: any;

  constructor(
    private studentService: StudentService,
    private snackBar: MatSnackBar
  ) {}
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.getStudents();
  }

  public getStudents() {
    this.isLoading = true;
    this.studentService.getStudentsLit().subscribe(
      (res) => {
        this.isLoading = false;
        this.student = res;
        this.dataSource = new MatTableDataSource<Student>(this.student);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      (err: HttpErrorResponse) => {
        console.log(err);
      }
    );
  }

  public removeHandler(id: number) {
    if (confirm('مطمئنید میخواهید حدف کنید ؟')) {
      this.studentService.removeStudent(id).subscribe((res) => {
        this.getStudents();
        this.snackBar.open('', '! با موفقیت حذف شد ', {
          duration: 5000,
          verticalPosition: 'top',
        });
      });
    } else {
      this.getStudents();
    }
  }
}
