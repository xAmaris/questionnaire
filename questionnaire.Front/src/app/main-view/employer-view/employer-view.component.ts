import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-employer-view',
  templateUrl: './employer-view.component.html',
  styleUrls: ['./employer-view.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmployerViewComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
