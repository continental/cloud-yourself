import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddAwsAccountComponent } from './views/add-aws-account/add-aws-account.component';
import { ShowAwsAccountComponent } from './views/show-aws-account/show-aws-account.component';

const routes: Routes = [{
  path: 'add/:url',
  component: AddAwsAccountComponent
}, {
  path: 'show/:url',
  component: ShowAwsAccountComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AwsAccountRoutingModule { }
