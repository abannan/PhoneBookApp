export interface IContactDetail {
  id: number;
  contactType: number;
  contactValue: string;
}

export interface IContactPerson {
  id: number;
  firstName: string;
  lastName: string;
  contactDetails: IContactDetail[];
}
