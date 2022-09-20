import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ExampleComponent} from './example/example.component';
import {PrimengModule} from "../primeng/primeng.module";

@NgModule({
  declarations: [ExampleComponent],
  imports: [CommonModule, PrimengModule],
  exports: [ExampleComponent],
})
export class ComponentsModule {
}
