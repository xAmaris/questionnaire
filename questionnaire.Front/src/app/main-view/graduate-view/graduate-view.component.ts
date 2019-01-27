import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-graduate-view',
  templateUrl: './graduate-view.component.html',
  styleUrls: ['./graduate-view.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush

})
export class GraduateViewComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
