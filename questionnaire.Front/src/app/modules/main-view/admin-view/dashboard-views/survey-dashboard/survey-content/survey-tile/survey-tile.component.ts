import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'app-survey-tile',
  templateUrl: './survey-tile.component.html',
  styleUrls: ['./survey-tile.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SurveyTileComponent {
  @Input()
  survey;
  constructor() {}
}
