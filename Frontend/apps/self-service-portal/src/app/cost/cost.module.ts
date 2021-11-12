import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MaterializeUiCoreModule } from 'ngx-materialize-ui-core';
import { CostRoutingModule } from './cost-routing.module';
import { SharedModule } from '../shared/shared.module';
import { CostMatrixComponent } from './views/cost-matrix/cost-matrix.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterializeUiCoreModule,
    CostRoutingModule,
    SharedModule
  ],
  declarations: [
    CostMatrixComponent
  ]
})
export class CostModule { }
