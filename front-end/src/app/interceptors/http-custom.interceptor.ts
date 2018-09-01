import { Injectable } from "@angular/core";
import {
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse,
  HttpHandler,
  HttpEvent
} from "@angular/common/http";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/do";
import "rxjs/add/operator/catch";
import "rxjs/add/observable/throw";

import { MatSnackBar } from "@angular/material";

@Injectable()
export class HttpCustomInterceptor implements HttpInterceptor {
  constructor(public snackBar: MatSnackBar) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // Add content-type for POST, PUT, PATCH automatically
    if (
      request.method === "POST" ||
      request.method === "PUT" ||
      request.method === "PATCH"
    ) {
      request = request.clone({
        setHeaders: {
          "Content-Type": "application/json; charset=utf-8"
        }
      });
    }

    return next.handle(request).catch(response => {
      let message: string;

      if (response instanceof HttpErrorResponse) {
        switch (response.status) {
          case 0: {
            message = "No network connection";
            break;
          }
          case 401: {
            message = "Unauthenticated";
            break;
          }
          case 403: {
            message = "Unauthorized";
            break;
          }
          case 404: {
            message = "Resource Not Found";
            break;
          }
          case 500: {
            message = "Server error";
            break;
          }
          default: {
            message = response.status.toString();
            break;
          }
        }
      }

      this.snackBar.open(message, null, {
        duration: 2000
      });

      return Observable.throw(response);
    });
  }
}
