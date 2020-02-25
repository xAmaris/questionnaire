import { Component, OnDestroy, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { SharedService } from '../../../shared/services/shared.service';

@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.scss']
})
export class AdminViewComponent implements OnInit, OnDestroy {
  constructor(
    private sharedService: SharedService,
    private titleService: Title
  ) {}
  ngOnInit() {
    this.titleService.setTitle('Ankietyzator - admin');
    this.showAdminMenu();
  }
  showAdminMenu() {
    this.sharedService.showAdminMain(true);
  }
  ngOnDestroy() {}
}
