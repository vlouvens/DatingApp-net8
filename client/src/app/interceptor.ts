import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AppInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const clonedReq = req.clone({
      setHeaders: {
        Authorization: `Bearer YOUR_TOKEN`
      }
    });
    return next.handle(clonedReq);
  }
}
