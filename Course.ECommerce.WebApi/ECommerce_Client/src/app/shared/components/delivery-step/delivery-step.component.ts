import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-delivery-step',
  templateUrl: './delivery-step.component.html',
  styleUrls: ['./delivery-step.component.scss'],
  providers: [{provide: CdkStepper, useExisting: DeliveryStepComponent}],
})
export class DeliveryStepComponent extends CdkStepper implements OnInit {

  @Input() linearSelected:boolean;
  ngOnInit(): void {
    this.linear = this.linearSelected
  }

  // selectStepByIndex(index: number): void {
  //   this.selectedIndex = index;
  // }
}
