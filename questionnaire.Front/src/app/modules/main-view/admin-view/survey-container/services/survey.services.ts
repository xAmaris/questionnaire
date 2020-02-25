import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Subject } from 'rxjs/internal/Subject';
import { AppConfig } from '../../../../../core/others/url.config';
import { Update } from '../models/survey-creator.models';
import { SurveySurvey, SurveyTemplate } from '../models/survey.model';
import { map } from 'rxjs/operators';

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
  constructor(private http: HttpClient, private config: AppConfig) {}

  saveSurveyAnswer(survey, id, hash) {
    return this.http
      .post<any>(this.config.apiUrl + '/surveyanswer/' + hash, {
        SurveyTitle: survey.title,
        SurveyId: id,
        Questions: survey.questions
      })
      .pipe(
        map(data => {
          return data;
        })
      );
  }
  sendSurvey(survey) {
    return this.http
      .post<any>(this.config.apiUrl + '/email/emails', {
        Subject: survey.Subject,
        Body: survey.Body
      })
      .pipe(
        map(data => {
          return data;
        })
      );
  }
  sendSpecificSurvey(id: number): Observable<any> {
    return this.http
      .post<any>(this.config.apiUrl + '/email/survey-emails/' + id, {})
      .pipe(
        map(data => {
          return data;
        })
      );
  }
  createSurvey(survey) {
    return this.http
      .post<any>(this.config.apiUrl + '/surveytemplate/surveys', {
        Title: survey.title,
        Questions: survey.questions
      })
      .pipe(
        map(data => {
          return data;
        })
      );
  }
  updateSurvey(object: Update): Observable<any> {
    return this.http
      .put<Update>(this.config.apiUrl + '/surveytemplate/surveys', {
        surveyId: object.id,
        Title: object.Title,
        Questions: object.QuestionTemplates
      })
      .pipe(
        map(data => {
          return data;
        })
      );
  }
  deleteSurvey(id: number) {
    return this.http
      .delete<any>(this.config.apiUrl + '/surveytemplate/' + id)
      .pipe(
        map(data => {
          return data;
        })
      );
  }
  getAllSurveys(): Observable<SurveyTemplate[]> {
    return this.http
      .get<SurveyTemplate[]>(this.config.apiUrl + '/surveytemplate/surveys')
      .pipe(
        map(data => {
          return data;
        })
      );
  }
  getAllSentSurveys(): Observable<any[]> {
    return this.http.get<any[]>(this.config.apiUrl + '/survey/surveys').pipe(
      map(data => {
        return data;
      })
    );
  }
  getSurveyTemplateWithId(id: number): Observable<SurveyTemplate> {
    return this.http
      .get<SurveyTemplate>(this.config.apiUrl + '/surveytemplate/' + id)
      .pipe(
        map(data => {
          return data;
        })
      );
  }
  getSentSurveyWithId(id: number): Observable<SurveySurvey> {
    return this.http
      .get<SurveySurvey>(this.config.apiUrl + '/survey/' + id)
      .pipe(
        map(data => {
          return data;
        })
      );
  }
  getSurveyWithIdAndHash(id: number, hash: string): Observable<any> {
    return this.http
      .get<any>(this.config.apiUrl + '/survey/' + id + '/' + hash)
      .pipe(
        map(data => {
          return data;
        })
      );
  }
  getSurveyReport(id: number): Observable<any> {
    return this.http
      .get<any>(this.config.apiUrl + '/surveyreport/surveyReports/' + id)
      .pipe(
        map(data => {
          return data;
        })
      );
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
}
