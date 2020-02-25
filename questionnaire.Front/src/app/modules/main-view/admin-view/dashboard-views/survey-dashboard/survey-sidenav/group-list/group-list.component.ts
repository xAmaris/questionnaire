import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-group-list',
  templateUrl: './group-list.component.html',
  styleUrls: ['./group-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GroupListComponent implements OnInit {
@Input() groupList;
  constructor() {}

  ngOnInit() {}
}
