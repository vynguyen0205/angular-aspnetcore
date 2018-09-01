import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { Observable } from "rxjs/Observable";
import { of } from "rxjs/observable/of";
import { catchError, map, tap } from "rxjs/operators";

import { Contact } from "../models/contact.model";
import { Constants } from "../shared/Constants";
import { ContactList } from "../models/contact-list.model";

@Injectable()
export class ContactService {
  constructor(private http: HttpClient) {}

  getContacts(
    keyWord: string,
    tagId: number,
    itemsPerPage: number,
    pageNo: number
  ): Observable<ContactList> {
    const url = `${
      Constants.apiContacts
    }?keyWord=${keyWord}&tagId=${tagId}&itemsPerPage=${itemsPerPage}&pageNo=${pageNo}`;

    return this.http
      .get<ContactList>(url)
      .pipe(catchError(this.handleError("getContacts", new ContactList())));
  }

  getContact(contactId: number): Observable<Contact> {
    const url = `${Constants.apiContacts}/${contactId}`;

    return this.http
      .get<Contact>(url)
      .pipe(catchError(this.handleError("getContact", null)));
  }

  modifyContactTag(contactId: number, params): Observable<any> {
    const url = `${Constants.apiContacts}/${contactId}`;

    return this.http
      .patch(url, params)
      .pipe(catchError(this.handleError("modifyContactTag", null)));
  }

  private handleError<T>(operation = "operation", result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); // log to console instead
      return of(result as T);
    };
  }
}
