import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './components/authen/register/register.component';
import { LoginComponent } from './components/authen/login/login.component';
import { AuthenService } from './services/authen.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './auth/auth.interceptor';
import { SharedModule } from './components/shared/shared.module';
import { LayoutComponent } from './components/layout/layout.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    LayoutComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, SharedModule],
  providers: [
    AuthenService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor, //from auth/auth.interceptor.ts
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
