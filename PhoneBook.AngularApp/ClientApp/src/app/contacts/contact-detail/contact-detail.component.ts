import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IContactPerson, IContactDetail } from '../interfaces'

@Component({
  selector: 'app-contact-detail',
  templateUrl: './contact-detail.component.html',
  styleUrls: ['./contact-detail.component.css']
})
export class ContactDetailComponent implements OnInit {
  public addMode: boolean = true;
  id: number;

  private _contactPerson: IContactPerson = <IContactPerson>{
    id: 0,
    firstName: '',
    lastName: '',
    contactDetails: []
    };

  form: FormGroup = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    telephone: ['', Validators.required]
  });

  constructor(private _http: HttpClient, @Inject('BASE_URL') private _baseUrl: string, private fb: FormBuilder, private _router: Router, private route: ActivatedRoute) {
    this.id = this.route.snapshot.params['id'];

    if (!this.id) {
      return;
    }
    this.addMode = false;

    this.getData();
}

  ngOnInit(): void {
  }

  getData() {
    const header = {
      'Application-User-Id': '1'
    };

    const requestOptions = {
      headers: new HttpHeaders(header),
    };

    this._http.get<IContactPerson>(this._baseUrl + 'people/' + this.id, requestOptions).subscribe(result => {

      var data = {
        firstName: result.firstName,
        lastName: result.lastName,
        telephone: result.contactDetails[0].contactValue
      };
      this.form.patchValue(data);

      this._contactPerson = result;

    }, error => console.error(error));
  }

  onSubmit() {
    this._contactPerson.firstName = this.form.value.firstName;
    this._contactPerson.lastName = this.form.value.lastName;

    var temp = this._contactPerson.contactDetails[0];

    if (!temp) {
      temp = {
        id: 0,
        contactType: 1,
        contactValue: ""
      };
    }

    temp.contactType = 1;
    temp.contactValue = this.form.value.telephone;

    this._contactPerson.contactDetails[0] = temp;

    const header = {
      'Application-User-Id': '1'
    };

    const requestOptions = {
      headers: new HttpHeaders(header),
    };

    if (this.addMode) {
      this._http.post(this._baseUrl + 'people', this._contactPerson, requestOptions).subscribe(result => {
        console.log(result);

        this._router.navigate(["contacts"]);
      });
      return;
    }

    this._http.put(this._baseUrl + 'people', this._contactPerson, requestOptions).subscribe(result => {
      console.log(result);

        this._router.navigate(["contacts"]);
    });
  }

}

