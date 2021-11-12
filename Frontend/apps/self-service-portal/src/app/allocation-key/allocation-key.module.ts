import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MaterializeUiCoreModule } from 'ngx-materialize-ui-core';
import { AllocationKeyRoutingModule } from './allocation-key-routing.module';
import { CreateAllocationKeyComponent } from './views/create-allocation-key/create-allocation-key.component';
import { DisplayAllocationKeyComponent } from './views/display-allocation-key/display-allocation-key.component';
import { EditAllocationKeyComponent } from './views/edit-allocation-key/edit-allocation-key.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule, 
    MaterializeUiCoreModule,
    AllocationKeyRoutingModule
  ],
  declarations: [
    CreateAllocationKeyComponent,
    DisplayAllocationKeyComponent,
    EditAllocationKeyComponent
  ]
})
export class AllocationKeyModule { }
