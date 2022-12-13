import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ContactsListComponent } from './contacts/contacts-list/contacts-list.component';
import { ContactDetailComponent } from './contacts/contact-detail/contact-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ContactsListComponent,
    ContactDetailComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: ContactsListComponent, pathMatch: 'full' },
      { path: 'contacts', component: ContactsListComponent },
      { path: 'contactdetail', component: ContactDetailComponent },
      { path: 'contactdetail/:id', component: ContactDetailComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
