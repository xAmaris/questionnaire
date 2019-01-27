import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  UnregisteredUser,
  UnregisteredUserModel
} from '../../../../../../models/user.model';
import { UserService } from '../../../../survey-container/services/user.services';

@Component({
  selector: 'app-add-user-dialog',
  templateUrl: './add-user-dialog.component.html',
  styleUrls: ['./add-user-dialog.component.scss']
})
export class AddUserDialogComponent implements OnInit {
  loader = false;
  selected = 0;

  constructor(private fb: FormBuilder, private userService: UserService) {}

  ngOnInit() {}
  onSubmit(form) {
    if (form.valid) {
      const value: UnregisteredUser = form.value;
      console.log(value);
      this.loader = true;
      const unregUser: UnregisteredUserModel = new UnregisteredUserModel(value);
      console.log(unregUser);
      this.userService.saveUnregisteredUser(unregUser).subscribe(
        data => {
          console.log(data);
          this.loader = false;
        },
        error => {
          console.log(error);
          this.loader = false;
        }
      );
    }
  }
}
