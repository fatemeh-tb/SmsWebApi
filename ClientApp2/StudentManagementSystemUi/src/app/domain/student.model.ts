import { Course } from "./course.model";
import { CourseStudent } from "./coursestudent.model";
import { Score } from "./score.model";

export class Student {
  id:number;
  fName: string;
  lName: string;
  phone: number;
  nationalCode: number;
  parentName: string;
  gender: string;
  imagePath: string;
  scores: Score[]
  courseStudent: CourseStudent[];
}
