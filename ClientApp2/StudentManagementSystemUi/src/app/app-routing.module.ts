import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddCourseComponent } from './course/add-course/add-course.component';
import { CourseDetailsComponent } from './course/course-details/course-details.component';
import { CourseComponent } from './course/course.component';
import { MainpageComponent } from './mainpage/mainpage.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ScoresComponent } from './scores/scores.component';
import { AddStudentComponent } from './student/add-student/add-student.component';
import { StudentDetailsComponent } from './student/student-details/student-details.component';
import { StudentComponent } from './student/student.component';

const routes: Routes = [
  { path: '', component: MainpageComponent },
  { path: 'studentslist', component: StudentComponent },
  { path: 'addnewstudent', component: AddStudentComponent },
  { path: 'courseslist', component: CourseComponent },
  { path: 'addnewcourse', component: AddCourseComponent },
  { path: 'score/:courseId/:studentId', component: ScoresComponent },
  { path: 'studentslist/:id/:name', component: StudentDetailsComponent },
  { path: 'courseslist/:id/:name', component: CourseDetailsComponent },
  {path: '404', component: NotFoundComponent},
  {path: '**', redirectTo: '/404'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
