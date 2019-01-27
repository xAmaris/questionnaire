import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import {
  MainFormSurvey,
  MainFormTemplate,
  QuestionData
} from '../main-view/admin-view/survey-container/models/survey-creator.models';
import {
  FieldDataSurvey,
  FieldDataTemplate,
  QuestionSurvey,
  QuestionTemplate
} from '../main-view/admin-view/survey-container/models/survey.model';
import { SurveyService } from '../main-view/admin-view/survey-container/services/survey.services';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-survey-viewform',
  templateUrl: './survey-viewform.component.html',
  styleUrls: ['./survey-viewform.component.scss']
})
export class SurveyViewformComponent implements OnInit, OnDestroy {
  invoiceForm: FormGroup;
  inputErrorArr: number[] = [];
  defaultQuestion = 'Brak pytania';
  loader = false;
  showError = false;
  submitted = false;
  id: number;
  hash: string;
  isPreviewed: boolean;
  title: string;
  defaultError = 'Odpowiedź na to pytanie jest wymagana';
  singleGridError = 'To pytanie wymaga jednej odpowiedzi w każdym wierszu';
  multipleGridError =
    'To pytanie wymaga co najmniej jednej odpowiedzi w każdym wierszu';
  // subs
  surveyIDSub: Subscription = new Subscription();

  nameof = <T>(name: keyof T) => name;

  constructor(
    private surveyService: SurveyService,
    private fb: FormBuilder,
    private router: Router,
    private sharedService: SharedService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getSurvey();
    this.showUserInfo();
    this.sharedService.showSendButton(true);
  }
  showAdminMenu(x): void {
    this.sharedService.showAdminMain(x);
  }
  showUserInfo(): void {
    this.sharedService.showUser(false);
  }
  getSurvey(): void {
    this.activatedRoute.data.map(data => data.cres).subscribe(
      res => {
        if (res) {
          const params = this.activatedRoute.snapshot.params;
          console.log(res);
          this.loader = true;
          this.id = Number(params['id']);
          this.hash = params['hash'];
          if (params['preview'] === 's') {
            this.isPreviewed = true;
          } else if (params['preview'] === 't') {
            this.isPreviewed = false;
          }
          // this.preview = this.activatedRoute.snapshot.params['preview'];
          this.createQuestionData(res);
          this.title = res['title'];
          this.surveyService.isCreatorLoading(false);
          if (!this.hash) {
            this.showBackButton(true);
            this.showPreview(true);
          } else {
            this.showAdminMenu(false);
          }
        }
      },
      error => {
        console.log(error);
        this.surveyService.isCreatorLoading(false);
      }
    );
  }
  showBackButton(x: boolean): void {
    this.sharedService.showBackButton(x);
  }
  showPreview(x: boolean): void {
    this.sharedService.showPreviewDiv(x);
  }
  sendSurvey(): void {
    if (this.invoiceForm.valid) {
      this.showError = false;
      this.surveyService
        .saveSurveyAnswer(this.invoiceForm.getRawValue(), this.id, this.hash)
        .subscribe(
          data => {
            console.log(data);
          },
          error => {
            console.log(error);
            const nameArr: string[] = Object.keys(error.error);
            // console.log(nameArr);
            nameArr.forEach((err: string) => {
              const n: number = err.indexOf('[') + 1;
              this.inputErrorArr.push(Number(err.charAt(n)));
            });
            // console.log(this.inputErrorArr);
            // this.inputErrorArr = error;
            // error.error.forEach(err => {

            // });
            this.submitted = true;
          }
        );
    } else {
      this.showError = true;
      console.log(this.invoiceForm);
    }
  }

  routeToSurveyCompleted() {
    this.router.navigateByUrl('./formResponse');
  }
  routeToEditSurvey(): void {
    this.router.navigateByUrl('/app/admin/survey/create/' + this.id);
  }

  ngOnDestroy(): void {
    this.sharedService.showSendButton(false);
    if (!this.hash) {
      this.showBackButton(false);
      this.showPreview(false);
    }
    // else {
    this.showAdminMenu(true);
    // }
  }

  updateSelection(choiceOptions, radio, e?): void {
    // console.log(choiceOptions);
    choiceOptions.forEach(el => {
      el.controls.value.setValue(false);
    });
    radio.setValue(true);
    if (e) {
      e.source.checked = true;
    }
  }

  sth(f) {
    console.log(f);
  }

  createQuestionData(data) {
    let propertyName;
    this.invoiceForm = this.fb.group({
      title: [data.title],
      // Created_Date: [data.Created_Date],
      // Created_Time: [data.Created_Time],
      questions: this.fb.array([])
    });
    propertyName =
      this.hash || this.isPreviewed
        ? this.nameof<MainFormSurvey>('questions')
        : this.nameof<MainFormTemplate>('questionTemplates');
    data[propertyName].forEach(question => {
      this.createQuestion(question);
    });
  }

  createQuestion(question) {
    const control: FormArray = this.invoiceForm.get(`questions`) as FormArray;
    const group = this.addRows(question);
    control.push(group);
  }

  addRows(question: QuestionData) {
    const group = this.fb.group({
      content: [question.content || this.defaultQuestion],
      select: [question.select],
      QuestionPosition: [question.QuestionPosition],
      FieldData: this.fb.array([]),
      isRequired: question.isRequired
    });
    this.createFieldData(question, group.controls);
    return group;
  }

  createFieldData(question, controls) {
    const propertyName: string =
      this.hash || this.isPreviewed
        ? this.nameof<QuestionSurvey>('fieldData')
        : this.nameof<QuestionTemplate>('fieldDataTemplates');
    question[propertyName].forEach(data => {
      this.addGroup(
        controls.FieldData,
        controls.select.value,
        controls.isRequired.value,
        data
      );
    });
  }

  addGroup(FieldData, select: string, isRequired: boolean, data) {
    switch (select) {
      case 'short-answer':
      case 'long-answer':
        this.addInput(FieldData, isRequired);
        break;
      case 'linear-scale':
        this.createRadio(FieldData, data, isRequired);
        break;
      case 'dropdown-menu':
      case 'single-choice':
      case 'multiple-choice':
        // this.addCheckField(FieldData, data);
        this.addArray(FieldData, isRequired, data);
        break;
      case 'single-grid':
      case 'multiple-grid':
        this.createRow(FieldData, data, isRequired);
        break;
    }
  }

  addArray(FieldData, isRequired: boolean, data) {
    const group = this.fb.group({
      choiceOptions: this.fb.array([])
    });
    FieldData.push(group);
    const propertyName =
      this.hash || this.isPreviewed
        ? this.nameof<FieldDataSurvey>('choiceOptions')
        : this.nameof<FieldDataTemplate>('choiceOptionTemplates');
    data[propertyName].forEach(choiceOptions => {
      // this.addCheckField(group.controls.choiceOptions, choiceOptions);
      this.createViewValue(
        group.controls.choiceOptions,
        choiceOptions.viewValue,
        choiceOptions.optionPosition,
        isRequired
      );
    });
  }
  addInput(FieldData, isRequired) {
    const group = this.fb.group({
      input: ''
    });
    if (isRequired) {
      // group.controls.input.setValidators([Validators.required]);
    }
    FieldData.push(group);
  }
  addCheckField(selectArr, data) {
    // console.log();
    const group = this.fb.group({
      // input: false,
      value: false,
      viewValue: [data.viewValue]
    });
    selectArr.push(group);
  }

  // grid
  // rows
  createRow(fieldData, oldFieldData, isRequired) {
    const group = this.fb.group({
      rows: this.fb.array([])
    });
    fieldData.push(group);
    const propertyName =
      this.hash || this.isPreviewed
        ? this.nameof<FieldDataSurvey>('rows')
        : this.nameof<FieldDataTemplate>('rowTemplates');
    const rowLength = oldFieldData[propertyName].length;

    for (let i = 0; i < rowLength; i++) {
      this.createGrid(
        group.controls.rows,
        oldFieldData,
        i,
        isRequired,
        propertyName
      );
    }
  }

  createGrid(rows, oldFieldData, i, isRequired, propertyName) {
    const group = this.fb.group({
      rowPosition: oldFieldData[propertyName][i].rowPosition,
      input: oldFieldData[propertyName][i].input,
      choiceOptions: this.fb.array([])
    });
    rows.push(group);
    const propertyNameChoice =
      this.hash || this.isPreviewed
        ? this.nameof<FieldDataSurvey>('choiceOptions')
        : this.nameof<FieldDataTemplate>('choiceOptionTemplates');
    const colLength = oldFieldData[propertyNameChoice].length;
    for (let j = 0; j < colLength; j++) {
      this.createViewValue(
        group.controls.choiceOptions,
        oldFieldData[propertyNameChoice][j].viewValue,
        oldFieldData[propertyNameChoice][j].optionPosition,
        isRequired
      );
    }
  }
  createViewValue(field, name, pos, isRequired) {
    const group = this.fb.group({
      ChoicePosition: pos,
      viewValue: name,
      value: false
    });
    if (isRequired) {
      group.controls.value.setValidators([Validators.required]);
    }
    field.push(group);
  }
  //

  // linear
  createRadio(fieldData, oldFieldData, isRequired) {
    const minValue = oldFieldData.minValue;
    const maxValue = oldFieldData.maxValue;
    const group = this.fb.group({
      minLabel: oldFieldData.minLabel,
      maxLabel: oldFieldData.maxLabel,
      choiceOptions: this.fb.array([])
    });
    fieldData.push(group);
    let index = 0;
    for (let i = minValue; i <= maxValue; i++) {
      this.createViewValue(
        group.controls.choiceOptions,
        i.toString(),
        index,
        isRequired
      );
      index++;
    }
  }
  inputFieldError(formGroup: FormGroup): boolean {
    const control = formGroup.controls.input;
    if (control.errors && this.showError) {
      console.log('g');
      return true;
    }
  }
  see(x) {
    console.log(x);
  }
  controlError(question, i) {
    const bool1 = this.controlErrorFromApi(i);
    const bool2 = this.controlEmpty(question);
    return bool1 && bool2;
  }
  controlErrorFromApi(i): boolean {
    // console.log()
    return this.inputErrorArr.includes(i) && this.submitted;
  }
  controlEmpty(question: any): boolean {
    const field = question.controls.FieldData.controls[0];
    const select = question.value.select;
    let isEmpty = false;
    switch (select) {
      case 'short-answer':
      case 'long-answer':
        if (field.controls.input.value.length === 0) {
          isEmpty = true;
          return isEmpty;
        }
        return isEmpty;
      case 'dropdown-menu':
      case 'single-choice':
      case 'multiple-choice':
      case 'linear-scale':
        const controlArr = field.controls.choiceOptions.controls;
        let broke = false;
        controlArr.forEach(val => {
          if (val.value.value === true) {
            broke = true;
            return;
          }
        });
        if (!broke) {
          isEmpty = true;
        }
        return isEmpty;
      case 'single-grid':
      case 'multiple-grid':
        let gridBroke = false;
        const rowArr = field.controls.rows.controls;
        rowArr.forEach(row => {
          const colArr = row.controls.choiceOptions.controls;
          colArr.forEach(col => {
            if (col.value.value === true) {
              gridBroke = true;
              return;
            }
          });
          if (gridBroke) {
            return;
          }
        });
        if (!gridBroke) {
          isEmpty = true;
        }
        return isEmpty;
      default:
        return isEmpty;
    }
  }
}
