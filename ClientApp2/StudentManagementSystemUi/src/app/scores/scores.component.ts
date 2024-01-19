import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Course } from '../domain/course.model';
import { Score } from '../domain/score.model';
import { Student } from '../domain/student.model';
import { CourseService } from '../service/course.service';
import { ScoreService } from '../service/score.service';
import { StudentService } from '../service/student.service';

@Component({
  selector: 'app-scores',
  templateUrl: './scores.component.html',
  styleUrls: ['./scores.component.css'],
})
export class ScoresComponent implements OnInit {
  scores: Score[] = [];
  studentScore: Score[] = [];
  studentInfo: Student[] = [];
  courseInfo: Course[] = [];
  studentId: number;
  courseId: number;
  scoreId: number;
  isScoreAdded: boolean;
  formGroup: FormGroup;

  scoreDetails: Score[] = [];

  constructor(
    private scoreService: ScoreService,
    private studentService: StudentService,
    private courseService: CourseService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getStudentScore();
    this.route.params.subscribe((params) => {
      this.studentId = params['studentId'];
      this.courseId = params['courseId'];
    });
    this.studentService.getStudentById(this.studentId).subscribe((result) => {
      this.studentInfo.push(result);
    });
    this.courseService.getCourseById(this.courseId).subscribe((result) => {
      this.courseInfo = result.title;
    });

    this.formGroup = new FormGroup({
      id: new FormControl(''),
      score1: new FormControl('', [
        Validators.required,
        Validators.pattern('^[0-9]*$'),
      ]),
      score2: new FormControl('', [
        Validators.required,
        Validators.pattern('^[0-9]*$'),
      ]),
      score3: new FormControl('', [
        Validators.required,
        Validators.pattern('^[0-9]*$'),
      ]),
      score4: new FormControl('', [
        Validators.required,
        Validators.pattern('^[0-9]*$'),
      ]),
      finalScore: new FormControl('', [
        Validators.required,
        Validators.pattern('^[0-9]*$'),
      ]),
    });
  }

  public getStudentScore() {
    this.isScoreAdded = false;
    this.scoreService.getStudentScore().subscribe((res) => {
      this.scores = res;
      this.studentScore = [];
      var scores = this.scores.find((x) => x.studentId == this.studentId);
      if (scores != undefined) {
        this.studentScore.push(scores);
        this.isScoreAdded = true;
      }
    });
  }

  public editHandler() {
    if (this.studentScore != []) {
      this.studentScore.forEach((i) => {
        this.scoreId = i.id;
      });
    }
    this.scoreService.getScoresById(this.scoreId).subscribe((result) => {
      this.scoreDetails.push(result);
      this.scoreDetails.forEach((element) => {
        this.formGroup.setValue({
          id: element.id,
          score1: element.score1,
          score2: element.score2,
          score3: element.score3,
          score4: element.score4,
          finalScore: element.finalScore,
        });
      });
    });
  }

  public onSubmit(scoreFormValue) {
    const formValues = { ...scoreFormValue };
    const score: Score = {
      id: this.scoreId,
      score1: formValues.score1,
      score2: formValues.score2,
      score3: formValues.score3,
      score4: formValues.score4,
      finalScore: formValues.finalScore,
      studentId: this.studentId,
      courseId: this.courseId,
    };

    if (this.isScoreAdded === false) {
      this.scoreService.addStudentScore(score).subscribe((res) => {
        this.isScoreAdded = true;
        window.location.reload();
      });
    } else {
      this.scoreService.updateStudent(score).subscribe((res) => {
        window.location.reload();
      });
    }
  }
}
