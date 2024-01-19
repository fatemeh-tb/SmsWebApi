import { CourseStudent } from "./coursestudent.model";
import { Score } from "./score.model";

export class Course {
  id: number
  title: string;
  courseCode: number;
  courseStudent: CourseStudent[];
  scores: Score[]
}
