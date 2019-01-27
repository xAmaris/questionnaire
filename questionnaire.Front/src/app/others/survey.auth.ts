import { Injectable} from '@angular/core';
import { CanLoad, Router } from '@angular/router';
import { SurveyService } from '../main-view/admin-view/survey-container/services/survey.services';

@Injectable()
export class SurveyGuard implements CanLoad {
  bool = false;
  constructor(private router: Router, private surveyService: SurveyService) {
    // this.surveyService.savedSurvey.subscribe(data => {
    //   if (data) {
    //     this.bool = true;
    //   }
    // });
  }

  canLoad() {
    if (this.bool === true) {
      return true;
    }
    this.router.navigateByUrl('app/admin/create');
    return false;
  }
}
