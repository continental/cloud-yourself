import { Component } from '@angular/core';
import { ResourceBase, ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-list-managed-resources',
  templateUrl: './list-managed-resources.component.html'
})
export class ListManagedResourcesComponent extends ViewBase {

  _onDeploy(managedResourceVm: ResourceBase): void {
  }

}
