import { Component, OnInit } from '@angular/core';
import { SurveyService } from '../dashboard-views/services/survey.services';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard-content',
  templateUrl: './dashboard-content.component.html',
  styleUrls: ['./dashboard-content.component.scss']
})
export class DashboardContentComponent implements OnInit {
  constructor(private surveyService: SurveyService, private router: Router) {}

  ngOnInit() {}
  redirectToNew(): void {
    this.surveyService.isCreatorLoading(true);
    const obj = {
      title: '',
      questions: [] as any
    };
    this.surveyService.createSurvey(obj).subscribe(
      data => {
        const string: string = '/app/survey/create/' + data;
        this.router.navigateByUrl(string);
      },
      error => {
        console.log(error);
      }
    );
  }
}
