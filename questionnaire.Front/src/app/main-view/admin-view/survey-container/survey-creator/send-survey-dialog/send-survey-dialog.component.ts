import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { SurveyService } from '../../services/survey.services';

@Component({
  selector: 'app-send-survey-dialog',
  templateUrl: './send-survey-dialog.component.html',
  styleUrls: ['./send-survey-dialog.component.scss']
})
export class SendSurveyDialogComponent implements OnInit {
  dialogForm: FormGroup;
  subject: AbstractControl;
  url: string;
  toMailTable: string;
  accent = 'accent';
  loader = false;

  constructor(
    private dialog: MatDialogRef<SendSurveyDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private surveyService: SurveyService,
    private fb: FormBuilder
  ) {
    this.url =
      'http://localhost:4200/app/admin/survey/viewform/s/' + this.data.id;
  }

  ngOnInit() {
    // form declaration
    this.dialogForm = this.fb.group({
      subject: ['']
    });

    // connecting controls with form inputs
    this.subject = this.dialogForm.controls['subject'];

    // console.log(this.data);
    this.toMailTable = `
    <div style="padding: 10px;">
  <table width="100%">
    <tbody>
      <tr id="header">
        <td height="40" style="background-color: #8bc34a; padding: 25px; text-align: center; color: white;">
        ${this.data.content.title}
        </td>
      </tr>
      <tr>
        <td height="30"></td>
      </tr>
      <tr>
        <td style="text-align: center;"><span>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
            tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation
            ullamco laboris nisi ut aliquip ex ea commodo consequat.</span></td>
      </tr>
      <tr>
        <td height="30"></td>
      </tr>
      <tr>
      <tr>
        <td style="text-align: center; font-weight: 600;"><span>Zapraszamy do wypełnienia ankiety</span></td>
      </tr>
      <tr>
        <td height="30"></td>
      </tr>
      <tr style="display: flex; justify-content: center;">
        <td style="display: flex; justify-content: center;">
          <div style="border-radius: 3px; background-color: #8bc34a; padding: 10px; text-align: center; max-width: 150px;">
            <a href=${
              this.url
            } style="text-decoration: none; color: white; outline: none;">Przejdź do ankiety</a>
          </div>
        </td>
      </tr>
      <tr></tr>
    </tbody>
  </table>
</div>`;
  }

  onSubmit(value) {
    // const sendObj = {
    //   Subject: value.subject,
    //   Body: this.toMailTable
    // };
    // console.log(sendObj);
    // this.loader = true;
    // this.surveyService.sendSurvey(sendObj).subscribe(
    //   data => {
    //     console.log(data);
    //     this.loader = false;
    //     this.dialog.close();
    //   },
    //   error => {
    //     console.log(error);
    //   }
    // );
    this.loader = true;
    this.surveyService.sendSpecificSurvey(this.data.id).subscribe(
      data => {
        console.log(data);
        this.loader = false;
        this.dialog.close();
      },
      error => {
        this.loader = false;
        console.log(error);
      }
    );
  }
}
