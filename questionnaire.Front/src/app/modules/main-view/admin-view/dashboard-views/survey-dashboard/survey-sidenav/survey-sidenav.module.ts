import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GroupListModule } from './group-list/group-list.module';
import { SurveySearchModule } from './survey-search/survey-search.module';
import { SurveySidenavComponent } from './survey-sidenav.component';

export const routes: Routes = [{ path: '', component: SurveySidenavComponent }];

@NgModule({
  imports: [
    CommonModule,
    GroupListModule,
    SurveySearchModule,
    RouterModule.forChild(routes)
  ],
  declarations: [SurveySidenavComponent]
})
export class SurveySidenavModule {}
