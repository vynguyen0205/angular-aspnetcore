import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ContactDetailComponent } from './contact-detail/contact-detail.component';

const routes: Routes = [
    { path: 'contacts/:contactId', component: ContactDetailComponent }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [
        RouterModule
    ]
})

export class AppRoutingModule { }