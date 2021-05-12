import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ResourceBase } from 'fancy-hateoas-client';

@Component({
  selector: 'app-form-action',
  templateUrl: './form-action.component.html'
})
export class FormActionComponent {

  @Input()
  form: NgForm | null = null;

  @Input()
  model: ResourceBase | null = null;

  @Input()
  action: string | null = null;

  @Output()
  executed = new EventEmitter();

  _onTap() {
    if(this.form?.valid && this.model && this.action) {
      this.model[this.action]().then(() => this.executed.next());
    }
  }
}
