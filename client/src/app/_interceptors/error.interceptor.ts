import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/internal/operators/catchError';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const toast = inject(ToastrService);
  return next(req).pipe(
    catchError( error =>{
      if(error){
        switch(error.status){
          case 400 :
            if(error.error.errors){
              const modelStateErrors = [];
              for(const key in error.error.errors){
                if(error.error.errors[key]){
                  modelStateErrors.push(error.error.errors[key]);
                }
              }
              throw modelStateErrors.flat();
            }else{
              toast.error(error.error);
            }
            break;
          case 401:
            toast.error('Unautorized',error.status);
            break;
            case 404:
              router.navigateByUrl('/not-found'); 
              break;
            case 500: 
              const navigationExtras:NavigationExtras = {state:{error:error.error}};
              router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              toast.error('Something unexpected went wrong');
              break;
      }
    }throw error;
 } )
  );
};
