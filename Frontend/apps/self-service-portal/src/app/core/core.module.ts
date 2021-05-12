import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MaterializeUiCoreModule } from 'ngx-materialize-ui-core';
import { HomeComponent } from './components/home/home.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MaterializeUiCoreModule
  ],
  declarations: [HomeComponent],
  exports: [HomeComponent]
})
export class CoreModule { }
