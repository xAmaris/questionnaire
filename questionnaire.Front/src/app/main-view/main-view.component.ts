import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { UserProfile } from '../auth/other/user.model';
import { AccountService } from '../auth/services/account.service';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-main-view',
  templateUrl: './main-view.component.html',
  styleUrls: ['./main-view.component.scss']
  // changeDetection: ChangeDetectionStrategy.OnPush
})
export class MainViewComponent implements OnInit {
  constructor(
    private sharedService: SharedService,
    private accountService: AccountService
  ) {}
  ngOnInit() {
    this.showUserInfo();
    this.setProfileData();
  }
  setProfileData() {
    this.accountService.setProfileData();
  }
  showUserInfo() {
    this.sharedService.showUser(true);
  }
}
