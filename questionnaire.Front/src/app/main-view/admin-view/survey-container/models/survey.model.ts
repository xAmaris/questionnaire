// tslint:disable:max-classes-per-file
class Survey {
  title: string;
  id: number;
  createdAt: string;
  constructor(title: string, id: number, createdAt?: string) {
    this.title = title;
    this.id = id;
    this.createdAt = createdAt;
  }
}
export class SurveySurvey extends Survey {
  questions: QuestionSurvey[];
}
export class SurveyTemplate extends Survey {
  questionTemplates: QuestionTemplate[];
}

export class SurveyModel extends Survey {
  created_date: string;
  created_time: string;
  questionTemplates: Question[];

  constructor(survey: Survey) {
    super(survey.title, survey.id);
    this.created_date = survey.createdAt
      .split('T')[0]
      .split('-')
      .reverse()
      .join('-');
    this.created_time = survey.createdAt.split('T')[1].split('.')[0];
  }
}

class Question {
  id: number;
  surveyId: number;
  questionPosition: number;
  isRequired: boolean;
  content: string;
  select: string;
}
export class QuestionSurvey extends Question {
  fieldData: FieldDataSurvey[];
}
export class QuestionTemplate extends Question {
  fieldDataTemplates: FieldDataTemplate[];
}

class FieldData {
  id: number;
  input: string;
  maxLabel: string;
  minLabel: string;
  maxValue: number;
  minValue: number;
  questionId: number;
}
export class FieldDataSurvey extends FieldData {
  choiceOptions: Choice[];
  rows: Row[];
}
export class FieldDataTemplate extends FieldData {
  choiceOptionTemplates: Choice[];
  rowTemplates: Row[];
}

export class Row {
  id: number;
  input: string;
  fieldDataId: number;
  rowPosition: number;
}

export class Choice {
  id: number;
  optionPosition: number;
  value: boolean;
  viewValue: string;
  fieldDataId: number;
}
