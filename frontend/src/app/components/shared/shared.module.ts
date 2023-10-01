import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SpinnerComponent } from './spinner/spinner.component';
import { HeaderComponent } from './header/header.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MenuComponent } from './menu/menu.component';

@NgModule({
  declarations: [HeaderComponent, SpinnerComponent, MenuComponent],
  imports: [CommonModule],
  exports: [
    CommonModule,
    HeaderComponent,
    MenuComponent,
    SpinnerComponent,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
  ],
})
export class SharedModule {}
