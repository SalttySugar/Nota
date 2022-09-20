import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TabViewModule} from "primeng/tabview";
import {ButtonModule} from "primeng/button";


@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [
    TabViewModule,
    ButtonModule
  ]
})
export class PrimengModule {
}
