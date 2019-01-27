import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { SurveyService } from '../services/survey.services';

@Injectable()
export class SurveyViewformResolver implements Resolve<any> {
  constructor(private surveyService: SurveyService) {}
  resolve(route: ActivatedRouteSnapshot): Observable<any> {
    this.surveyService.isCreatorLoading(true);
    const id = Number(route.params['id']);
    const hash = route.params['hash'];
    const preview = route.params['preview'];
    if (hash && preview === 's') {
      return this.surveyService.getSurveyWithIdAndHash(id, hash);
    } else {
      if (preview === 's') {
        return this.surveyService.getSentSurveyWithId(id);
      } else if (preview === 't') {
        return this.surveyService.getSurveyTemplateWithId(id);
      }
    }
  }
}
