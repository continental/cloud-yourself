import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CostMatrixComponent } from './views/cost-matrix/cost-matrix.component';

const routes: Routes = [{
  path: 'cost-matrix/:url',
  component: CostMatrixComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CostRoutingModule { }
