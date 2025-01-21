import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SettingsRoutingModule } from './settings-routing.module';
import { SettingsOverviewComponent } from './settings-overview/settings-overview.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [SettingsOverviewComponent],
  imports: [
    CommonModule,
    SettingsRoutingModule,
    FormsModule
  ]
})
export class SettingsModule { }
