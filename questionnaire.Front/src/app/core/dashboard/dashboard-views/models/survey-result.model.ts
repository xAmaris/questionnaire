// tslint:disable:max-classes-per-file

export class Result {
  title: string;
  totalAnswerCount: number;
  questionsData: ResultQuestion[];
}

export class ResultQuestion {
  question: string;
  select: string;
  answerCount: number;
  answerData: ResultAnswer;
}

export class ResultAnswer {
  labels: string[] | number[];
  datasets: ResultData[];
}

export class ResultData {
  label: string;
  data: string[] | number[];
  backgroundColor: string | string[];
}
