import { Course } from './course.model';
import { Student } from './student.model';

export class CourseStudent {
  coursesId: number;
  course: Course;
  studentsId: number;
  student: Student;
}
