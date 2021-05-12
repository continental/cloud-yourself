import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FormActionComponent } from './components/form-action/form-action.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule
  ],
  declarations: [
    FormActionComponent
  ],
  exports: [
    FormActionComponent
  ]
})
export class SharedModule { }
