import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Subject } from 'rxjs/internal/Subject';
import { Observable } from 'rxjs/Observable';
import { AppConfig } from '../../../../app.config';
import { Update } from '../models/survey-creator.models';
import { SurveySurvey, SurveyTemplate } from '../models/survey.model';

@Injectable()
export class SurveyService {
  controlArray: string[];
  savedSurveys: BehaviorSubject<SurveyTemplate[]> = new BehaviorSubject<
    SurveyTemplate[]
  >(undefined);
  savedSentSurveys: BehaviorSubject<any[]> = new BehaviorSubject<any[]>(
    undefined
  );
  openingCreatorLoader: Subject<boolean> = new Subject<boolean>();
  // openedSurvey: any;
  filterSurveyListInput: Subject<string> = new Subject<string>();
  constructor(private http: HttpClient, private config: AppConfig) {}

  saveSurveyAnswer(survey, id, hash) {
    // const obj = {
    //   SurveyTitle: survey.title,
    //   SurveyId: id,
    //   Questions: survey.questions
    // };
    // console.log(JSON.stringify(obj));
    return this.http
      .post<any>(this.config.apiUrl + '/surveyanswer/' + hash, {
        SurveyTitle: survey.title,
        SurveyId: id,
        Questions: survey.questions
      })
      .map(data => {
        return data;
      });
  }
  sendSurvey(survey) {
    return this.http
      .post<any>(this.config.apiUrl + '/email/emails', {
        Subject: survey.Subject,
        Body: survey.Body
      })
      .map(data => {
        return data;
      });
  }
  sendSpecificSurvey(id: number): Observable<any> {
    return this.http
      .post<any>(this.config.apiUrl + '/email/survey-emails/' + id, {})
      .map(data => {
        return data;
      });
  }
  createSurvey(survey) {
    // const obj = {
    //   Title: survey.title,
    //   Questions: survey.questions
    // };
    // console.log(JSON.stringify(obj));
    return this.http
      .post<any>(this.config.apiUrl + '/surveytemplate/surveys', {
        Title: survey.title,
        Questions: survey.questions
      })
      .map(data => {
        return data;
      });
  }
  updateSurvey(object: Update): Observable<any> {
    // const obj = {
    //   surveyId: object.id,
    //   Title: object.Title,
    //   Questions: object.Questions
    // };
    // console.log(JSON.stringify(obj));
    return this.http
      .put<Update>(this.config.apiUrl + '/surveytemplate/surveys', {
        surveyId: object.id,
        Title: object.Title,
        Questions: object.QuestionTemplates
      })
      .map(data => {
        return data;
      });
  }
  deleteSurvey(id: number) {
    return this.http
      .delete<any>(this.config.apiUrl + '/surveytemplate/' + id)
      .map(data => {
        return data;
      });
  }
  getAllSurveys(): Observable<SurveyTemplate[]> {
    return this.http
      .get<SurveyTemplate[]>(this.config.apiUrl + '/surveytemplate/surveys')
      .map(data => {
        return data;
      });
  }
  getAllSentSurveys(): Observable<any[]> {
    return this.http
      .get<any[]>(this.config.apiUrl + '/survey/surveys')
      .map(data => {
        return data;
      });
  }
  getSurveyTemplateWithId(id: number): Observable<SurveyTemplate> {
    return this.http
      .get<SurveyTemplate>(this.config.apiUrl + '/surveytemplate/' + id)
      .map(data => {
        return data;
      });
  }
  getSentSurveyWithId(id: number): Observable<SurveySurvey> {
    return this.http
      .get<SurveySurvey>(this.config.apiUrl + '/survey/' + id)
      .map(data => {
        return data;
      });
  }
  getSurveyWithIdAndHash(id: number, hash: string): Observable<any> {
    return this.http
      .get<any>(this.config.apiUrl + '/survey/' + id + '/' + hash)
      .map(data => {
        return data;
      });
  }
  getSurveyReport(id: number): Observable<any> {
    return this.http
      .get<any>(this.config.apiUrl + '/surveyreport/surveyReports/' + id)
      .map(data => {
        return data;
      });
  }
  saveSurveysFromApi(): void {
    this.getAllSurveys().subscribe(data => {
      this.savedSurveys.next(data);
    });
  }
  saveSentSurveysFromApi(): void {
    this.getAllSentSurveys().subscribe(data => {
      this.savedSentSurveys.next(data);
    });
  }
  isCreatorLoading(x: boolean): void {
    this.openingCreatorLoader.next(x);
  }
  filterSurveyList(x: string): void {
    this.filterSurveyListInput.next(x);
  }
  // openCreator(formGroup): void {
  //   this.openedSurvey = formGroup;
  // }
  // getSurveyToOpen() {
  //   return this.openedSurvey;
  // }

  saveInLocaLStorage() {}
}
