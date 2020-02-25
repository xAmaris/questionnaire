import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'app-survey-sent-tile',
  templateUrl: './survey-sent-tile.component.html',
  styleUrls: ['./survey-sent-tile.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SurveySentTileComponent {
  @Input()
  survey;
  constructor() {}
}
