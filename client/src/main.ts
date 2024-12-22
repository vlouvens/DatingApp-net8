import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.routes';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AppInterceptor } from './app/interceptor';
import { InjectionToken } from '@angular/core';

export const APP_CONFIG = new InjectionToken<typeof appConfig>('app.config')

// bootstrapApplication(AppComponent, appConfig)
//   .catch((err) => console.error(err));

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(withInterceptorsFromDi()),
    { provide: HTTP_INTERCEPTORS, useClass: AppInterceptor, multi: true },
    {provide : APP_CONFIG, useValue : appConfig}
  ]
}).catch(err => console.error(err));
