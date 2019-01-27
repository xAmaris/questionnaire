import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { SurveyTemplate } from '../models/survey.model';
import { SurveyService } from '../services/survey.services';

@Injectable()
export class SurveyCreatorResolver implements Resolve<SurveyTemplate> {
  constructor(private surveyService: SurveyService) {}
  resolve(route: ActivatedRouteSnapshot): Observable<SurveyTemplate> {
    this.surveyService.isCreatorLoading(true);
    return this.surveyService.getSurveyTemplateWithId(Number(route.params['id']));
  }
}
