import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';

import { NgProgressModule } from '@ngx-progressbar/core';
import { NgProgressHttpModule } from '@ngx-progressbar/http';

import { CustomMaterialModule } from '../shared/material.module';
import { AppRoutingModule } from './app.routing.module';

import { AppComponent } from './app.component';
import { HttpCustomInterceptor } from '../interceptors/http-custom.interceptor';
import { ContactService } from '../services/contact.service';
import { ContactDetailComponent } from './contact-detail/contact-detail.component';
import { ContactItemComponent } from './contact-item/contact-item.component';
import { TagService } from '../services/tag.service';
import { ConfirmDeleteComponent } from './contact-item/confirm-delete/confirm-delete.component';


@NgModule({
  declarations: [
    AppComponent,
    ContactDetailComponent,
    ContactItemComponent,
    ConfirmDeleteComponent
  ],

  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,

    FlexLayoutModule,
    CustomMaterialModule,
    NgProgressModule.forRoot(),
    NgProgressHttpModule,

    AppRoutingModule
  ],

  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpCustomInterceptor,
      multi: true
    },
    HttpClient,
    ContactService,
    TagService
  ],

  bootstrap: [
    AppComponent
  ],

  entryComponents: [
    ConfirmDeleteComponent
  ]
})

export class AppModule { }
