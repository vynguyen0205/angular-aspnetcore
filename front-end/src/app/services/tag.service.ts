import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { Observable } from "rxjs/Observable";
import { of } from "rxjs/observable/of";
import { catchError, map, tap } from "rxjs/operators";

import { Constants } from "../shared/Constants";
import { Tag } from "../models/tag.model";

@Injectable()
export class TagService {
  constructor(private http: HttpClient) {}

  getTags(): Observable<Array<Tag>> {
    const url = Constants.apiTags;

    let result = this.http
      .get<Array<Tag>>(url)
      .pipe(catchError(this.handleError("getTags", [])));

    return result;
  }

  addTag(tagName: string): Observable<Tag> {
    const url = Constants.apiTags;

    return this.http
      .post<Tag>(url, { name: tagName })
      .pipe(catchError(this.handleError("addTag", null)));
  }

  updateTag(tagId: number, name: string): Observable<Tag> {
    const url = `${Constants.apiTags}/${tagId}`;

    return this.http
      .put<Tag>(url, { name: name })
      .pipe(catchError(this.handleError("updateTag", null)));
  }

  deleteTag(tagId: number): Observable<any> {
    const url = `${Constants.apiTags}/${tagId}`;

    return this.http
      .delete<any>(url)
      .pipe(catchError(this.handleError("deleteTag", null)));
  }

  private handleError<T>(operation = "operation", result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); // log to console instead
      return of(result as T);
    };
  }
}
