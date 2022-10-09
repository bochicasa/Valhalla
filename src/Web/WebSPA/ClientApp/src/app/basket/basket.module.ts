import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BasketComponent } from './basket.component';
import { BasketStatusComponent } from './basket-status/basket-status.component';



@NgModule({
  declarations: [
    BasketComponent,
    BasketStatusComponent
  ],
  imports: [
    CommonModule
  ]
})
export class BasketModule { }
