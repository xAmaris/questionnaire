import {
  Component,
  OnInit,
  ContentChild,
  TemplateRef,
  Input
} from '@angular/core';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  @ContentChild(TemplateRef)
  parentTemplate: any;
  @Input()
  list: any[];
  constructor() {}

  ngOnInit() {}

}
