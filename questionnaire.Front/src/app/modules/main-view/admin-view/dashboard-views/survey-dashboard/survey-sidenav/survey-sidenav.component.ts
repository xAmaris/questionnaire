import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-survey-sidenav',
  templateUrl: './survey-sidenav.component.html',
  styleUrls: ['./survey-sidenav.component.scss']
})
export class SurveySidenavComponent implements OnInit {
  groupList = [
    {
      name: 'studenci'
    },
    {
      name: 'pracodawcy'
    },
    {
      name: 'absolwenci'
    }
  ];
  constructor() {}

  ngOnInit() {}
}
