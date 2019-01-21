import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-survey-completed',
  templateUrl: './survey-completed.component.html',
  styleUrls: ['./survey-completed.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SurveyCompletedComponent implements OnInit {
  constructor() {}

  ngOnInit() {}
}
