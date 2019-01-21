// tslint:disable:max-classes-per-file
import { FormArray } from '@angular/forms';
import { QuestionTemplate } from './survey.model';

export class Control {
  value: any;
  viewValue: string;
  constructor(value: any, viewValue: string) {
    this.value = value;
    this.viewValue = viewValue;
  }
}
export class Select {
  control: Control[];
  constructor(control: Control[]) {
    this.control = control;
  }
}

export class Value {
  value: number;
  constructor(value: number) {
    this.value = value;
  }
}
export class Tooltip {
  add: string;
  copy: string;
  delete: string;
  constructor(add: string, copy: string, deletee: string) {
    this.add = add;
    this.copy = copy;
    this.delete = deletee;
  }
}

export class Update {
  id: number;
  Title: string;
  QuestionTemplates: QuestionTemplate[];
}

export class QuestionData {
  content: string;
  select: string;
  lastSelect: string;
  isRequired: boolean;
  QuestionPosition: number;
  FieldData: FormArray;
}
export class MoveDialogData {
  content: string;
  position: number;
}
export class ChoiceOptions {
  viewValue: string;
  value: boolean;
  optionPosition: number;
}
export class ChoiceOptionsData {
  viewValue: string;
  value: {
    value: boolean;
    disabled: boolean;
  };
  optionPosition: number;
}

export class MainForm {
  title: string;
  // description: string;
  questionTemplates: FormArray;
}
export class MainFormSurvey extends MainForm {
  questions: FormArray;
}
export class MainFormTemplate extends MainForm {
  questionTemplates: FormArray;
}
export class RowData {
  rowPosition: number;
  input: string;
}
export class LinearData {
  minValue: number;
  maxValue: number;
  minLabel: string;
  maxLabel: string;
}
