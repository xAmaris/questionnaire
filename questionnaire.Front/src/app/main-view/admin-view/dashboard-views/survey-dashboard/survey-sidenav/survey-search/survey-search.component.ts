import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-survey-search',
  templateUrl: './survey-search.component.html',
  styleUrls: ['./survey-search.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SurveySearchComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
