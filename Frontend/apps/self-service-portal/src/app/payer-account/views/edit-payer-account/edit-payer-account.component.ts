import { Component, OnInit } from '@angular/core';
import { ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-edit-payer-account',
  templateUrl: './edit-payer-account.component.html'
})
export class EditPayerAccountComponent extends ViewBase {

  _onSave(valueObject: any) {
    valueObject.update().then(()=> this.reloadViewModel());
  }
  
}
