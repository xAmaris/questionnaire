import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatExpansionModule, MatTabsModule } from '@angular/material';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ProgressBarModule } from 'primeng/progressbar';
import { MaterialsModule } from '../../materials/materials.module';
import { MainSettingsComponent } from './main-settings/main-settings.component';
import { ProfileSettingsComponent } from './profile-settings/profile-settings.component';
import { SettingsComponent } from './settings.component';
import { SettingsRoutingModule } from './settings.routing';

@NgModule({
  imports: [
    CommonModule,
    SettingsRoutingModule,
    MatTabsModule,
    MaterialsModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    FormsModule,
    ProgressBarModule,
    MatExpansionModule
  ],
  declarations: [SettingsComponent, ProfileSettingsComponent, MainSettingsComponent]
})
export class SettingsModule {}
