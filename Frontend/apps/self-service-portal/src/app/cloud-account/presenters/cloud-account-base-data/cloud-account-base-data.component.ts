import { Component, Input, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ResourceBase } from 'fancy-hateoas-client';

@Component({
  selector: 'app-cloud-account-base-data',
  templateUrl: './cloud-account-base-data.component.html'
})
export class CloudAccountBaseDataComponent {

  @Input()
  public cloudAccountBaseData: ResourceBase = {}; 

  @ViewChild('form', { static: true })
  public form: NgForm | null = null;

  public get valid(): boolean {
    if(this.form && this.form.valid) {
      return true;
    }
    return false;
  }

}
