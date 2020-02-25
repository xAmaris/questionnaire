import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-content',
  templateUrl: './admin-content.component.html',
  styleUrls: ['./admin-content.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminContentComponent implements OnInit {
  constructor() {}

  ngOnInit() {}
}
