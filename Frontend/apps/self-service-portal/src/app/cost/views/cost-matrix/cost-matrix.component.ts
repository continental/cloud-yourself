import { Component } from '@angular/core';
import { ResourceBase, ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-cost-matrix',
  templateUrl: './cost-matrix.component.html'
})
export class CostMatrixComponent extends ViewBase {

  _onLoadCloudAccountCosts(payerAccountCost: ResourceBase) {
    if(!payerAccountCost.cloudAccountCosts) {
      // The cloud account costs are not yet loaded -> fetch the data
      payerAccountCost.fetch_cloudAccountCosts().then((data: any) => {
        payerAccountCost.cloudAccountCosts = data;
      });
    }
  }

  _onLoadCosts(cloudAccountCost: ResourceBase) {
    if(!cloudAccountCost.costs) {
      // The costs are not yet loaded -> fetch the data
      cloudAccountCost.fetch_costs().then((data: any) => {
        cloudAccountCost.costs = data;
      })
    }
  }

}
