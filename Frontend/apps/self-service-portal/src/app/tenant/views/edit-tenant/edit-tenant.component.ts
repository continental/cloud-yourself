import { Component, OnInit } from '@angular/core';
import { ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-edit-tenant',
  templateUrl: './edit-tenant.component.html'
})
export class EditTenantComponent extends ViewBase {

  _onSave(valueObject: any) {
    valueObject.update().then(()=> this.reloadViewModel());
  }

}
