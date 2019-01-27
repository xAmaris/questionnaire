import { Component, OnDestroy, OnInit } from '@angular/core';
import { SharedService } from '../../services/shared.service';

@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.scss']
})
export class AdminViewComponent implements OnInit, OnDestroy {
  constructor(private sharedService: SharedService) {}
  ngOnInit() {
    this.showAdminMenu();
  }
  showAdminMenu() {
    this.sharedService.showAdminMain(true);
  }
  ngOnDestroy() {}
}
