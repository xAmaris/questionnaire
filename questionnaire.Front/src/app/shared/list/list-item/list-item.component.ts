import {
  Component,
  OnInit,
  EventEmitter,
  Input,
  Output,
  ContentChild,
  TemplateRef
} from '@angular/core';

@Component({
  selector: 'app-list-item',
  templateUrl: './list-item.component.html',
  styleUrls: ['./list-item.component.scss']
})
export class ListItemComponent implements OnInit {
  @ContentChild('mainTmpl')
  mainTemplate: TemplateRef<any>;
  @ContentChild('menuTmpl')
  menuTemplate: TemplateRef<any>;
  @Input()
  item: any;
  @Output()
  itemClick: EventEmitter<any> = new EventEmitter<any>();

  constructor() {}

  ngOnInit() {}
  onItemClick() {}
}
