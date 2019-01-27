import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControlName,
  FormGroup
} from '@angular/forms';
import { MatDialog } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
// tslint:disable-next-line:no-submodule-imports no-implicit-dependencies
import * as cloneDeep from 'lodash/cloneDeep';
import { Subject } from 'rxjs/internal/Subject';
import { Observable } from 'rxjs/Observable';
import { debounceTime, switchMap } from 'rxjs/operators';
import { Subscription } from 'rxjs/Subscription';
import { SharedService } from '../../../../services/shared.service';
import { ConfirmDialogComponent } from '../../../../shared/confirm-dialog/confirm-dialog.component';
import {
  ChoiceOptions,
  ChoiceOptionsData,
  LinearData,
  MainForm,
  MoveDialogData,
  QuestionData,
  RowData,
  Select,
  Tooltip,
  Update,
  Value
} from '../models/survey-creator.models';
import {
  Choice,
  FieldDataTemplate,
  QuestionTemplate,
  Row,
  SurveyTemplate
} from '../models/survey.model';
import { SurveyService } from '../services/survey.services';
import { SendSurveyDialogData } from './../../../../data/shared.data';
import { MoveQuestionDialogComponent } from './move-question-dialog/move-question-dialog.component';
// import { SendSurveyDialogComponent } from './send-survey-dialog/send-survey-dialog.component';

@Component({
  selector: 'app-survey-creator',
  templateUrl: './survey-creator.component.html',
  styleUrls: ['./survey-creator.component.scss']
})
export class SurveyCreatorComponent
  implements OnInit, OnDestroy, AfterViewInit {
  invoiceForm: FormGroup;
  default = 'short-answer';
  autofocus = false;
  disabled = true;
  index = 0;
  questionIndex = 0;
  id: number;
  // subs
  showSurveySub: Subscription = new Subscription();
  createSurveySub: Subscription = new Subscription();
  surveyIDSub: Subscription = new Subscription();
  showSurveyDialogSub: Subscription = new Subscription();
  viewChildrenSub: Subscription = new Subscription();

  //
  private updateToApi = new Subject();
  public updateToApi$: Observable<any> = this.updateToApi.asObservable();
  //

  selects: Select[] = [
    {
      control: [
        { value: 'short-answer', viewValue: 'Krótka odpowiedź' },
        { value: 'long-answer', viewValue: 'Długa odpowiedź' }
      ]
    },
    {
      control: [
        { value: 'single-choice', viewValue: 'Jednokrotny wybór' },
        { value: 'multiple-choice', viewValue: 'Wielokrotny wybór' }
      ]
    },
    {
      control: [
        { value: 'dropdown-menu', viewValue: 'Menu rozwijane' },
        { value: 'linear-scale', viewValue: 'Skala liniowa' }
      ]
    },
    {
      control: [
        {
          value: 'single-grid',
          viewValue: 'Siatka jednokrotnego wyboru'
        },
        {
          value: 'multiple-grid',
          viewValue: 'Siatka wielokrotnego wyboru'
        }
      ]
    }
  ];

  minValue: Value[] = [{ value: 0 }, { value: 1 }];
  maxValue: Value[] = [
    { value: 2 },
    { value: 3 },
    { value: 4 },
    { value: 5 },
    { value: 6 },
    { value: 7 },
    { value: 8 },
    { value: 9 },
    { value: 10 }
  ];
  info: Tooltip = {
    add: 'Dodaj pytanie',
    copy: 'Duplikuj',
    delete: 'Usuń pytanie'
  };

  constructor(
    private fb: FormBuilder,
    private surveyService: SurveyService,
    private sharedService: SharedService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public dialog: MatDialog
  ) {}
  ngAfterViewInit() {
    this.autofocusField();
  }
  autofocusField() {
    // tslint:disable-next-line:no-this-assignment
    const that = this;
    const originFormControlNameNgOnChanges =
      FormControlName.prototype.ngOnChanges;
    FormControlName.prototype.ngOnChanges = function() {
      const result = originFormControlNameNgOnChanges.apply(this, arguments);
      if (this.name === 'viewValue' && that.autofocus === true) {
        this.control.nativeElement = this.valueAccessor._elementRef.nativeElement;
        this.control.nativeElement.focus();
        that.autofocus = false;
      }
      return result;
    };
  }
  ngOnInit(): void {
    this.subToObs();
    this.getSurvey();
    this.showSurveyOnClick();
    this.showSurveyDialog();
    this.showBackButton();
    this.sharedService.showCreatorButton(true);
  }
  getSurvey(): void {
    this.activatedRoute.data.map(data => data.cres).subscribe(
      (res: SurveyTemplate) => {
        this.surveyService.isCreatorLoading(false);
        if (res) {
          this.id = res.id;
          if (res.questionTemplates.length === 0) {
            this.createQuestionData();
            this.updateSurveySubject();
          } else if (res.questionTemplates.length > 0) {
            this.createQuestionData(res);
          }
        }
      },
      error => {
        this.surveyService.isCreatorLoading(false);
      }
    );
  }

  showBackButton(): void {
    this.sharedService.showBackButton(true);
  }
  showSurveyOnClick(): void {
    this.showSurveySub = this.sharedService.showButton.subscribe(() => {
      this.showSurvey();
    });
  }
  showSurveyDialog(): void {
    this.showSurveyDialogSub = this.sharedService.showSurveyDialog.subscribe(
      data => {
        if (data === true) {
          this.openSendSurveyDialog();
        }
      }
    );
  }
  openSendSurveyDialog(): void {
    this.openSurveyDialog().subscribe(res => {
      if (res) {
        this.sendSurveyToAll();
      }
    });
  }
  openSurveyDialog(): Observable<any> {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: new SendSurveyDialogData()
    });
    return dialogRef.afterClosed();
  }
  surveySendingLoading(x: boolean): void {
    this.sharedService.isSurveySendingLoading(x);
  }
  sendSurveyToAll() {
    this.surveySendingLoading(true);
    this.surveyService.sendSpecificSurvey(this.id).subscribe(
      data => {
        this.surveySendingLoading(false);
      },
      error => {
        this.surveySendingLoading(false);
      }
    );
  }
  updateSurveySubject(x?) {
    this.updateToApi.next(x);
  }
  subToObs() {
    this.updateToApi$
      .pipe(
        debounceTime(300),
        switchMap(() => this.updateSurveyObs())
      )
      .subscribe(() => {});
  }

  updateSurveyObs() {
    const object: Update = {
      Title: this.invoiceForm.getRawValue().title,
      QuestionTemplates: this.invoiceForm.getRawValue().questionTemplates,
      id: this.id
    };
    return this.surveyService.updateSurvey(object);
  }
  updateSurvey() {
    this.updateSurveyObs();
  }

  openMoveQuestionDialog(): void {
    const array: QuestionData[] = this.invoiceForm.getRawValue()
      .questionTemplates;
    /* KEYS: ["content", "QuestionPosition", "select", "lastSelect", "FieldData"]*/
    const nameArr = this.setPropertiesNames(array);
    this.openMoveDialog(array, nameArr).subscribe(res => {
      if (res) {
        this.invoiceForm.controls.questionTemplates = this.setPositionOnMove(
          this.invoiceForm.controls.questionTemplates,
          res,
          nameArr.position
        );
        this.updateSurveySubject();
      }
    });
  }

  openMoveControlDialog(question: FormGroup, arrayName: string): void {
    const array: ChoiceOptions[] = question.getRawValue().FieldData[0][
      arrayName
    ];
    /* KEYS: ["viewValue", "optionPosition", "value"] */
    const nameArr = this.setPropertiesNames(array);
    this.openMoveDialog(array, nameArr).subscribe(res => {
      if (res) {
        question.controls.FieldData['controls'][0].controls[
          arrayName
        ] = this.setPositionOnMove(
          question.controls.FieldData['controls'][0].controls[arrayName],
          res,
          nameArr.position
        );
        this.updateSurveySubject();
      }
    });
  }

  setPropertiesNames(array) {
    let nameArr;
    const propNameArr = Object.keys(array[0]);
    return (nameArr = {
      content: propNameArr[0],
      position: propNameArr[1]
    });
  }
  openMoveDialog(array, nameArr): Observable<any> {
    const dialogArr: MoveDialogData[] = [];
    array.forEach(el => {
      const obj: MoveDialogData = {
        content: el[nameArr.content],
        position: el[nameArr.position]
      };
      dialogArr.push(obj);
    });
    const dialogRef = this.dialog.open(MoveQuestionDialogComponent, {
      data: { content: dialogArr }
    });
    return dialogRef.afterClosed();
  }

  setPositionOnMove(array, res, controlName): FormArray {
    const clonedArr: FormArray = cloneDeep(array);
    const length: number = array.length;
    const moveList: AbstractControl[] = array.controls;
    const clonedMoveList: AbstractControl[] = clonedArr.controls;
    for (let i = 0; i < length; i++) {
      const id = res[i].position;
      clonedArr.setControl(i, moveList[id]);
      clonedMoveList[i]['controls'][controlName].setValue(i);
    }
    return clonedArr;
  }

  // creating FormGroup  -- Main Form
  createQuestionData(form?: SurveyTemplate): void {
    this.invoiceForm = this.fb.group(this.populateQuestionData(form));
    this.createQuestionField(form);
  }

  populateQuestionData(form?: SurveyTemplate): MainForm {
    let group: MainForm;
    if (form) {
      group = {
        title: form.title,
        questionTemplates: this.fb.array([])
      };
    } else {
      group = {
        title: 'Formularz bez nazwy',
        questionTemplates: this.fb.array([])
      };
    }
    return group;
  }
  createQuestionField(form?: SurveyTemplate): void {
    if (form) {
      const questionTemplates: QuestionTemplate[] = form.questionTemplates;
      const i = -1;
      questionTemplates.forEach((question: QuestionTemplate) => {
        this.addQuestion(i, question);
      });
    } else {
      this.addQuestion(-1);
    }
  }

  addQuestion(i: number, question?: QuestionTemplate): void {
    const group: FormGroup = this.addQuestionsControls(i, question);
    if (question) {
      const questionArr: FormArray = this.invoiceForm.controls
        .questionTemplates as FormArray;
      questionArr.push(group);
    } else {
      this.setNewQuestionPositions(i, group);
      this.updateSurveySubject();
    }
  }
  copyQuestion(question: FormGroup, index: number) {
    const clonedObject: FormGroup = cloneDeep(question);
    this.setNewQuestionPositions(index, clonedObject);
    this.updateSurveySubject();
  }
  setNewQuestionPositions(index: number, object?: FormGroup) {
    const questionArr: FormArray = this.invoiceForm.controls
      .questionTemplates as FormArray;
    const length: number = questionArr.length;
    const newIndex: number = index + 1;
    if (index !== length - 1) {
      const questionList: AbstractControl[] = questionArr.controls;
      for (let i: number = newIndex; i < length; i++) {
        questionList[i]['controls'].QuestionPosition.setValue(i + 1);
      }
    }
    if (object) {
      object.controls.QuestionPosition.setValue(newIndex);
      questionArr.insert(newIndex, object);
    }
  }
  removeQuestion(i: number): void {
    const questionArr: FormArray = this.invoiceForm.controls
      .questionTemplates as FormArray;
    const length: number = questionArr.controls.length;
    if (length > 1) {
      questionArr.removeAt(i);
      if (i !== length - 1) {
        this.setCurrentQuestionPositions(i, questionArr.controls, length - 1);
      }
    } else {
      questionArr.removeAt(0);
      const group = this.addQuestionsControls(-1);
      questionArr.push(group);
    }
    this.updateSurveySubject();
  }
  setCurrentQuestionPositions(
    index: number,
    array: AbstractControl[],
    length: number
  ): void {
    for (let i: number = index; i < length; i++) {
      array[i]['controls'].QuestionPosition.setValue(i);
    }
  }

  addQuestionsControls(i: number, question?: QuestionTemplate): FormGroup {
    const group: FormGroup = this.fb.group(
      this.populateQuestionsControls(i, question)
    );
    this.createFieldData(group.controls, question);
    return group;
  }
  populateQuestionsControls(
    i: number,
    question?: QuestionTemplate
  ): QuestionData {
    /* DO NOT CHANGE PROPERTIES ORDER */
    let group: QuestionData;
    if (question) {
      group = {
        content: question.content,
        QuestionPosition: question.questionPosition,
        select: question.select,
        isRequired: question.isRequired,
        lastSelect: undefined,
        FieldData: this.fb.array([])
      };
    } else {
      group = {
        content: '',
        QuestionPosition: i + 1,
        select: this.default,
        isRequired: true,
        lastSelect: undefined,
        FieldData: this.fb.array([])
      };
    }
    return group;
  }
  createFieldData(controls: any, question?: QuestionTemplate): void {
    if (question) {
      const fieldData: FieldDataTemplate[] = question.fieldDataTemplates;
      fieldData.forEach(data => {
        this.addGroup(controls.FieldData, controls.select.value, data);
      });
    } else {
      this.addGroup(controls.FieldData, controls.select.value);
    }
  }
  addGroup(
    fieldData: FormArray,
    select: string,
    data?: FieldDataTemplate | ChoiceOptions[]
  ): void {
    switch (select) {
      case 'short-answer':
      case 'long-answer':
        this.addInput(fieldData);
        break;
      case 'linear-scale':
        this.addLinearScaleField(fieldData, data);
        break;
      case 'dropdown-menu':
      case 'single-choice':
      case 'multiple-choice':
        this.addArray(fieldData, data);
        break;
      case 'single-grid':
      case 'multiple-grid':
        this.addSelectionGridField(fieldData, data);
        break;
    }
  }
  addArray(
    fieldData: FormArray,
    data?: FieldDataTemplate | ChoiceOptions[]
  ): void {
    const group: FormGroup = this.fb.group({
      choiceOptions: this.fb.array([])
    });
    this.createChoiceControls(
      group.controls.choiceOptions as FormArray,
      'opcja',
      data
    );
    fieldData.push(group);
  }
  createChoiceControls(
    choiceOptionsField: FormArray,
    name: string,
    data?: FieldDataTemplate | ChoiceOptions[]
  ): void {
    if (data) {
      const bool: boolean = this.isFieldData(data);
      if (bool) {
        const choiceData: Choice[] = (data as FieldDataTemplate)
          .choiceOptionTemplates;
        choiceData.forEach(choice => {
          this.addChoiceField(choiceOptionsField, name, choice);
        });
      } else {
        (data as ChoiceOptions[]).forEach(choice => {
          this.addChoiceField(choiceOptionsField, name, choice);
        });
      }
    } else {
      this.addChoiceField(choiceOptionsField, name);
      this.autofocus = true;
      this.updateSurveySubject();
    }
  }
  isFieldData(
    data: FieldDataTemplate | ChoiceOptions[]
  ): data is FieldDataTemplate {
    return (data as FieldDataTemplate).choiceOptionTemplates !== undefined;
  }
  addInput(fieldData: FormArray): void {
    const group: FormGroup = this.fb.group({
      input: [{ value: '', disabled: this.disabled }]
    });
    fieldData.push(group);
  }
  addSelectionGridField(
    fieldData: FormArray,
    data?: FieldDataTemplate | ChoiceOptions[]
  ): void {
    const group: FormGroup = this.fb.group({
      choiceOptions: this.fb.array([]),
      rows: this.fb.array([])
    });
    this.addField(
      group.controls.choiceOptions as FormArray,
      group.controls.rows as FormArray,
      data
    );
    fieldData.push(group);
  }

  addField(
    choiceData: FormArray,
    choiceData2: FormArray,
    data?: FieldDataTemplate | ChoiceOptions[]
  ): void {
    if (data) {
      const bool: boolean = this.isFieldData(data);
      if (bool) {
        const options: Choice[] = (data as FieldDataTemplate)
          .choiceOptionTemplates;
        const rowArr: Row[] = (data as FieldDataTemplate).rowTemplates;
        options.forEach(column => {
          this.addChoiceField(choiceData, 'kolumna', column);
        });
        rowArr.forEach(row => {
          this.addRowControl(choiceData2, 'wiersz', row);
        });
      } else {
        (data as ChoiceOptions[]).forEach(column => {
          this.addChoiceField(choiceData, 'kolumna', column);
        });
        this.addRowControl(choiceData2, 'wiersz');
      }
    } else {
      this.addChoiceField(choiceData, 'kolumna');
      this.addRowControl(choiceData2, 'wiersz');
    }
  }

  addLinearScaleField(
    choiceArr: FormArray,
    data?: FieldDataTemplate | ChoiceOptions[]
  ): void {
    const group = this.fb.group(this.populateLinearScale(data));
    choiceArr.push(group);
  }
  populateLinearScale(data?: FieldDataTemplate | ChoiceOptions[]) {
    let group: LinearData;
    if (data) {
      group = {
        minValue: (data as FieldDataTemplate).minValue,
        maxValue: (data as FieldDataTemplate).maxValue,
        minLabel: (data as FieldDataTemplate).minLabel,
        maxLabel: (data as FieldDataTemplate).maxLabel
      };
    } else {
      group = {
        minValue: 1,
        maxValue: 5,
        minLabel: '',
        maxLabel: ''
      };
    }
    return group;
  }
  addRowControl(selectArr: FormArray, name: string, data?: Row): void {
    const length: number = selectArr.controls.length;
    const group: FormGroup = this.fb.group(
      this.populateRowControl(name, length, data)
    );
    selectArr.push(group);
    if (!data) {
      this.updateSurveySubject();
      this.autofocus = true;
    }
  }
  populateRowControl(name: string, length: number, data?: Row): RowData {
    /* DO NOT CHANGE PROPERTIES ORDER */
    let group: RowData;
    if (data) {
      group = {
        input: data.input,
        rowPosition: data.rowPosition
      };
    } else {
      group = {
        input: `${name} ${length + 1}`,
        rowPosition: length
      };
    }
    return group;
  }
  addChoiceField(
    selectArr: FormArray,
    name: string,
    data?: ChoiceOptions | Choice
  ): void {
    const length: number = selectArr.controls.length;
    const group: FormGroup = this.fb.group(
      this.populateChoiceField(name, length, data)
    );
    selectArr.push(group);
  }
  populateChoiceField(
    name: string,
    length: number,
    data?: ChoiceOptions | Choice
  ): ChoiceOptionsData {
    /* DO NOT CHANGE PROPERTIES ORDER */
    let group: ChoiceOptionsData;
    if (data) {
      group = {
        viewValue: data.viewValue,
        optionPosition: (data as Choice).optionPosition,
        value: { value: false, disabled: this.disabled }
      };
    } else {
      group = {
        viewValue: `${name} ${length + 1}`,
        optionPosition: length,
        value: { value: false, disabled: this.disabled }
      };
    }
    return group;
  }

  removeField(index: number, formArr: FormArray, row?: boolean): void {
    const length: number = formArr.controls.length - 1;
    formArr.removeAt(index);
    if (index !== length) {
      this.setCurrentFieldPositions(
        index,
        formArr.controls as FormGroup[],
        length,
        row
      );
    }
    this.updateSurveySubject();
  }
  setCurrentFieldPositions(
    index: number,
    arr: FormGroup[],
    length: number,
    row?: boolean
  ): void {
    {
      if (!row) {
        for (let i: number = index; i < length; i++) {
          arr[i].controls.optionPosition.setValue(i);
        }
      } else {
        for (let i: number = index; i < length; i++) {
          arr[i].controls.rowPosition.setValue(i);
        }
      }
    }
  }
  saveLastSelect(lastSelect: AbstractControl, select: string): void {
    lastSelect.setValue(select);
  }

  changeControl(question: FormGroup): void {
    const fieldData: FormArray = question.controls.FieldData as FormArray;
    const select: string = question.value.select;
    const lastSelect: string = question.value.lastSelect;
    if (lastSelect !== select) {
      switch (lastSelect) {
        case 'single-choice':
        case 'multiple-choice':
        case 'dropdown-menu':
          this.choiceSwitch(select, fieldData);
          break;
        case 'single-grid':
        case 'multiple-grid':
          this.gridSwitch(select, fieldData);
          break;
        default:
          this.fieldRemoving(fieldData, select);
          break;
      }
      this.updateSurveySubject();
    }
  }
  choiceSwitch(select, fieldData) {
    switch (select) {
      case 'single-choice':
      case 'multiple-choice':
      case 'dropdown-menu':
        break;
      case 'single-grid':
      case 'multiple-grid':
        const control: ChoiceOptions[] = fieldData.controls[0][
          'controls'
        ].choiceOptions.getRawValue();
        this.fieldRemoving(fieldData, select, control);
        break;
      default:
        this.fieldRemoving(fieldData, select);
        break;
    }
  }
  gridSwitch(select, fieldData) {
    switch (select) {
      case 'single-grid':
      case 'multiple-grid':
        break;
      case 'single-choice':
      case 'multiple-choice':
      case 'dropdown-menu':
        const control: ChoiceOptions[] = fieldData.controls[0][
          'controls'
        ].choiceOptions.getRawValue();
        this.fieldRemoving(fieldData, select, control);
        break;
      default:
        this.fieldRemoving(fieldData, select);
        break;
    }
  }

  fieldRemoving(
    fieldData: FormArray,
    select: string,
    data?: ChoiceOptions[]
  ): void {
    const length: number = fieldData.controls.length - 1;
    for (let i = length; i >= 0; i--) {
      fieldData.removeAt(i);
    }
    this.addGroup(fieldData, select, data);
  }

  setSelection(e) {
    e.target.select();
  }
  createSurvey(): void {
    this.createSurveySub = this.surveyService
      .createSurvey(this.invoiceForm.getRawValue())
      .subscribe(
        () => {
          this.router.navigate(['/app/admin/d']);
        },
      );
  }

  showSurvey(): void {
    const string: string = '/app/admin/survey/viewform/t/' + this.id;
    this.router.navigateByUrl(string);
  }

  ngOnDestroy(): void {
    this.showSurveySub.unsubscribe();
    this.showSurveyDialogSub.unsubscribe();
    this.sharedService.showCreatorButton(false);
    this.sharedService.showBackButton(false);
  }
}
