import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { IContactPerson, IContactDetail } from '../interfaces'

@Component({
  selector: 'app-contacts-list',
  templateUrl: './contacts-list.component.html',
  styleUrls: ['./contacts-list.component.css']
})
export class ContactsListComponent {
  public contactList: IContactPerson[] = [];
  public searchCriteria: string = "";
  public refreshed: number = 0;

  private _doing: number = 0;

  constructor(private _http: HttpClient, @Inject('BASE_URL') private _baseUrl: string, private _router: Router) {
    
    this.handleSearch('');
  }

  public refreshList(urlPath: string) {

    this._doing = 1;

    const header = {
      'Application-User-Id': '1'
    };

    const requestOptions = {
      headers: new HttpHeaders(header),
    };

    this._http.get<IContactPerson[]>(this._baseUrl + urlPath, requestOptions).subscribe(result => {
      if (this._doing == 1) {
        return;
      }
      this.refreshed = 1;
      this.contactList = result;
      
    }, error => console.error(error));

    this._doing = 0;

  }

  public handleSearch(evt: string) {
    this.refreshed = 0;
    this.contactList = [];
    this.searchCriteria = evt;

    if (!evt && evt.length == 0) {
      this.refreshList('people');
      return;
    }

    if (evt.length <= 3) {
      return;
    }

    this.refreshList('people/search/' + evt);
  }

  public handleDelete(contact: IContactPerson) {

    if (!confirm('Are you sure you want to delete ' + contact.firstName + ' ' + contact.lastName + '?')) {
      return;
    }
    const header = {
      'Application-User-Id': '1'
    };

    const requestOptions = {
      headers: new HttpHeaders(header),
    };

    this._http.delete<IContactPerson[]>(this._baseUrl + 'people/' + contact.id, requestOptions).subscribe(result => {
      this.contactList = result;
      this.refreshed = 1;
    }, error => console.error(error));
  }

  public handleRowClick(contact: IContactPerson) {
    this._router.navigate(["contactdetail", contact.id]);
  }
}



