import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-move-question-dialog',
  templateUrl: './move-question-dialog.component.html',
  styleUrls: ['./move-question-dialog.component.scss']
})
export class MoveQuestionDialogComponent implements OnInit {
  length: number;
  defaultQuestion = 'Brak pytania';
  constructor(
    // private dialog: MatDialogRef<MoveQuestionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.length = this.data.content.length;
  }

  ngOnInit() {
    // console.log(this.data);
  }
  see(x) {
    console.log(x);
  }
}
