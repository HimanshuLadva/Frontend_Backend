import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest,} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {AuthService} from '@auth/service/auth.service';
import {RouteConfig} from '@shared/config/route-config';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {
  }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (err.status === 401 && !window.location.href.toLowerCase().includes(RouteConfig.auth_login.toLowerCase())) {
          // auto logout if 401 response returned from api
          this.authService.logout();
        }
        // const error = err.error.message || err.statusText;
        return throwError(err);
      })
    );
  }
}
