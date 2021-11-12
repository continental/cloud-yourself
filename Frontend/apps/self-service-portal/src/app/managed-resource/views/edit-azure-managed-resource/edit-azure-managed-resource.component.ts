import { Component } from '@angular/core';
import { ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-edit-azure-managed-resource',
  templateUrl: './edit-azure-managed-resource.component.html'
})
export class EditAzureManagedResourceComponent extends ViewBase {

  _onSave(valueObject: any) {
    valueObject.update().then(()=> this.reloadViewModel());
  }
  
}
