import { Component, OnInit } from '@angular/core';
import { basicTransition } from './other/router.animations';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss'],
  animations: [basicTransition]
})
export class AuthComponent implements OnInit {
  constructor() {}

  ngOnInit() {
  }
}
