import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HateoasClient, ResourceBase, ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-add-aws-account',
  templateUrl: './add-aws-account.component.html'
})
export class AddAwsAccountComponent extends ViewBase {

  _showSpinner = false;

  _selectedAccountId: string | null = null;

  _selectedAccount: ResourceBase | null = null;

  constructor(private router: Router, activatedRoute: ActivatedRoute, hateoasClient: HateoasClient) { 
    super(activatedRoute, hateoasClient);
  }

  set selectedAccountId(value: string) {
    this._selectedAccountId = value;
    // Find the subscription out of the options
    this._selectedAccount = this.viewModel.unmanagedAccounts.find((a: any) => a.awsAccountId === this._selectedAccountId);
  }

  get selectedAccountId(): string {
    return this._selectedAccountId ?? '';
  }

  _onAdd() {
    if(this._selectedAccount) {
      this._showSpinner = true;
      this._selectedAccount.add().then((resourceUrl: string) => {
        this.router.navigate(['/aws-accounts/show', resourceUrl[0]]);
      });
    } 
  }

}
