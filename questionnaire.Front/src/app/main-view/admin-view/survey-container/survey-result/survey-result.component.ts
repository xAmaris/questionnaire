import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { SharedService } from '../../../../services/shared.service';
import { SurveyService } from '../services/survey.services';

@Component({
  selector: 'app-survey-result',
  templateUrl: './survey-result.component.html',
  styleUrls: ['./survey-result.component.scss']
})
export class SurveyResultComponent implements OnInit, OnDestroy {
  loading = true;
  data;
  sth;
  id: number;
  // subs
  getResultsSub: Subscription = new Subscription();
  surveyIDSub: Subscription = new Subscription();
  color = '#8bc34a';
  colorData: string[] = [
    '#3366cc',
    '#dc3912',
    '#ff9900',
    '#109618',
    '#0099c6',
    '#dd4477',
    '#6a0',
    '#b82e2e',
    '#316395',
    '#994499',
    '#2a9',
    '#aa1',
    '#63c',
    '#e67300',
    '#8b0707',
    '#651067',
    '#329262',
    '#5574a6',
    '#3b3eac',
    '#b77322',
    '#16d620',
    '#b91383',
    '#f4359e',
    '#9c5935',
    '#a9c413',
    '#2a778d',
    '#668d1c',
    '#bea413',
    '#0c5922',
    '#743411',
    '#3366cc',
    '#dc3912'
  ];
  constructor(
    private surveyService: SurveyService,
    private sharedService: SharedService,
    private activatedRoute: ActivatedRoute
  ) {}
  ngOnInit() {
    this.getSurvey();
    this.sharedService.showBackButton(true);
  }
  getSurvey(): void {
    this.activatedRoute.data.map(data => data.cres).subscribe(
      res => {
        if (res) {
          this.createData(res);
          this.loading = false;
          this.surveyService.isCreatorLoading(false);
        }
      },
      error => {
        console.log(error);
        this.surveyService.isCreatorLoading(false);
      }
    );
  }
  createData(data) {
    this.data = {
      surveyTitle: data.surveyTitle,
      answersNumber: data.answersNumber,
      questionReports: this.populateQuestionReports(data.questionsReports)
    };
  }
  populateQuestionReports(reports) {
    const reportsArr = [];
    reports.forEach(report => {
      const rep = {
        content: report.content,
        select: report.select,
        answersNumber: report.answersNumber,
        answersData: this.populateAnswersData(
          report.labels,
          report.dataSets,
          report.select
        )
      };
      reportsArr.push(rep);
    });
    return reportsArr;
  }
  populateAnswersData(labels, dataSets, select) {
    const answerData = {
      labels,
      datasets: this.createDataSet(select, dataSets)
    };
    return answerData;
  }
  createDataSet(select, dataSets) {
    switch (select) {
      case 'short-answer':
      case 'long-answer':
      case 'linear-scale':
      case 'single-choice':
      case 'multiple-choice':
        return this.createMainDataSet(dataSets);
      case 'single-grid':
      case 'multiple-grid':
        return this.createGridDataSet(dataSets);
      case 'dropdown-menu':
        return this.createPieDataSet(dataSets);
    }
  }

  createMainDataSet(dataSets) {
    const dataSet = [
      {
        label: dataSets[0].label,
        data: dataSets[0]._data,
        backgroundColor: this.color
      }
    ];
    return dataSet;
  }
  createGridDataSet(dataSets) {
    let i = 0;
    const dataSetArr = [];
    dataSets.forEach(dataSet => {
      const set = {
        label: dataSet.label,
        data: dataSet._data,
        backgroundColor: this.colorData[i]
      };
      dataSetArr.push(set);
      i++;
    });
    return dataSetArr;
  }
  createPieDataSet(dataSets) {
    const _data = dataSets[0]._data;
    const dataSet = [
      {
        data: _data,
        backgroundColor: this.setColorArr(_data.length)
      }
    ];
    return dataSet;
  }
  setColorArr(length) {
    const colorArr = [];
    for (let i = 0; i < length; i++) {
      colorArr.push(this.colorData[i]);
    }
    return colorArr;
  }
  ngOnDestroy() {
    this.getResultsSub.unsubscribe();
    this.surveyIDSub.unsubscribe();
    this.sharedService.showBackButton(false);
  }
  see(x) {
    console.log(x);
  }
}
