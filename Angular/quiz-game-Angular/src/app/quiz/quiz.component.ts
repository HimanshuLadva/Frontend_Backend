import { Component, OnInit } from '@angular/core';
import { ApidataService } from '../apidata.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.scss'],
})
export class QuizComponent implements OnInit {
  constructor(
    private readonly quizApi: ApidataService,
    private readonly route: Router
  ) {}
  questionShow: boolean = true;
  currentQuestion: { question: string; options: string[] } = {
    question: '',
    options: [],
  };
  questionArray: {
    question: string;
    options: string[];
    answer: string;
    receiveAns: string;
  }[] = [];
  indexOfQuestionArray: number = 0;

  ngOnInit(): void {
    this.getQuestionsList();
    this.questionShow = false;
  }

  shuffleData(array: string[]) {
    for (let i = array.length - 1; i > 0; i--) {
      let j = Math.floor(Math.random() * (i + 1));
      [array[i], array[j]] = [array[j], array[i]];
    }

    return array;
  }

  getQuestionsList() {
    this.quizApi.getApiData().subscribe((data: any) => {
      if (data) {
        this.questionShow = true;
      }
      data.results.forEach(
        (ele: {
          question: string;
          correct_answer: string;
          incorrect_answers: string[];
          answer: string;
        }) => {
          this.questionArray.push({
            question: ele.question,
            options: this.shuffleData([
              ele.correct_answer,
              ...ele.incorrect_answers,
            ]),
            answer: ele.correct_answer,
            receiveAns: '',
          });
        }
      );
      this.currentQuestion = {
        question: this.questionArray[this.indexOfQuestionArray].question,
        options: [...this.questionArray[this.indexOfQuestionArray].options],
      };
    });
  }

  changeQuestion(option: string) {
    this.questionArray[this.indexOfQuestionArray].receiveAns = option; 
    this.indexOfQuestionArray = ++this.indexOfQuestionArray;
    if (this.indexOfQuestionArray == this.questionArray.length) {
      this.questionShow = false;
    }
    if (this.indexOfQuestionArray < this.questionArray.length) {
      this.currentQuestion = {
        question: this.questionArray[this.indexOfQuestionArray].question,
        options: [...this.questionArray[this.indexOfQuestionArray].options],
      };
    }
  }

  matchResult(id: number, option: string) {
    let arr = this.questionArray[id];
    if (arr.answer == option) {
      return 'green';
    } else if (arr.receiveAns == option) {
      return 'red';
    }
    return '';
  }

  getScore() {
    return this.questionArray.filter((ele) => ele.answer == ele.receiveAns)
      .length;
  }

  resetQuiz() {
    this.route.navigate(['']);
  }
}
