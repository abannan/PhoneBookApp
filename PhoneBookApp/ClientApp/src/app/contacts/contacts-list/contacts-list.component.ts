import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-contacts-list',
  templateUrl: './contacts-list.component.html',
  styleUrls: ['./contacts-list.component.css']
})
export class ContactsListComponent {
  public contactList: IContactPerson[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    const header = {
      'Application-User-Id': '1'
    };

    const requestOptions = {
      headers: new HttpHeaders(header),
    };

    http.get<IContactPerson[]>(baseUrl + 'people', requestOptions).subscribe(result => {
      this.contactList = result;
    }, error => console.error(error));
  }
}

interface IContactDetail {
  id: number;
  contactType: number;
  contactValue: string;
}

interface IContactPerson {
  id: number;
  firstName: string;
  lastName: string;
  contactDetails: IContactDetail[];
}


