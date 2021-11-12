import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateAllocationKeyComponent } from './views/create-allocation-key/create-allocation-key.component';
import { DisplayAllocationKeyComponent } from './views/display-allocation-key/display-allocation-key.component';
import { EditAllocationKeyComponent } from './views/edit-allocation-key/edit-allocation-key.component';

const routes: Routes = [{
  path: 'create/:url',
  component: CreateAllocationKeyComponent
}, {
  path: 'display/:url',
  component: DisplayAllocationKeyComponent
}, {
  path: 'edit/:url',
  component: EditAllocationKeyComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AllocationKeyRoutingModule { }
