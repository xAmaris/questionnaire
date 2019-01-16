import {
  Component,
  OnInit,
  Input,
  ChangeDetectionStrategy
} from '@angular/core';

@Component({
  selector: 'app-survey-list',
  templateUrl: './survey-list.component.html',
  styleUrls: ['./survey-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SurveyListComponent implements OnInit {
  @Input()
  surveyList: any;
  constructor() {}

  ngOnInit() {}
}
