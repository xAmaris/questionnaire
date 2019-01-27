import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  Inject,
  OnInit
} from '@angular/core';
import { MAT_SNACK_BAR_DATA } from '@angular/material';
import { SharedService } from './../../services/shared.service';

@Component({
  selector: 'app-survey-completed',
  templateUrl: './survey-completed.component.html',
  styleUrls: ['./survey-completed.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SurveyCompletedComponent implements OnInit {
  title = 'brak nazwy';
  constructor(
    private sharedService: SharedService,
    @Inject(MAT_SNACK_BAR_DATA) public data: any
  ) {}
  ngOnInit() {
    const savedTitle = this.sharedService.savedTitle;
    if (savedTitle) {
      this.title = savedTitle;
    }
  }
}
