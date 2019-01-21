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
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.length = this.data.content.length;
  }

  ngOnInit() {
  }
}
