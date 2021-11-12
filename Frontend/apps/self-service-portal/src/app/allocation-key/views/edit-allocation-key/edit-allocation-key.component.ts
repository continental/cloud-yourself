import { Component } from '@angular/core';
import { ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-edit-allocation-key',
  templateUrl: './edit-allocation-key.component.html'
})
export class EditAllocationKeyComponent extends ViewBase {

  _onSave(valueObject: any) {
    valueObject.update().then(()=> this.reloadViewModel());
  }
  
}
