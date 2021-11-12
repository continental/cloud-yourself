import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HateoasClient, ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-create-azure-managed-resource',
  templateUrl: './create-azure-managed-resource.component.html'
})
export class CreateAzureManagedResourceComponent extends ViewBase {

  constructor(private router: Router, activatedRoute: ActivatedRoute, hateoasClient: HateoasClient) { 
    super(activatedRoute, hateoasClient);
  }
  
  _onSave() {
    if(this.viewModel) {
      this.viewModel.create().then((resourceUrl: string) => {
        this.router.navigate(['/managed-resources/display/azure', resourceUrl[0]]);
      });
    } 
  }
  
}
