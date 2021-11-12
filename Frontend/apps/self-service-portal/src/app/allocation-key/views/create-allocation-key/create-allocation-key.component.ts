import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HateoasClient } from 'fancy-hateoas-client';
import { ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-create-allocation-key',
  templateUrl: './create-allocation-key.component.html'
})
export class CreateAllocationKeyComponent extends ViewBase {

  constructor(private router: Router, activatedRoute: ActivatedRoute, hateoasClient: HateoasClient) { 
    super(activatedRoute, hateoasClient);
  }
  
  _onSave() {
    if(this.viewModel) {
      this.viewModel.create().then((resourceUrl: string) => {
        this.router.navigate(['/allocation-keys/display', resourceUrl[0]]);
      });
    } 
  }
  
}
