import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Injectable } from "@angular/core";
import { error } from '@angular/compiler/src/util';
import { AppComponent } from '../app.component';

@Injectable({ providedIn: 'root' })
export class ApiService {

  content: any = {};

  constructor(private http: HttpClient, private appComponent: AppComponent) {
  }

  getData(url: string, id: any) {
    return this.http.get<any>(`${this.appComponent.APIURL}${url}`, { withCredentials: false })
      .pipe(map(content => {
        if (content == null || content.length <= 0) {
          
          return content;
        } else {
          this.content.data = content;
          // @ts-ignore
          this.content.fields = Object.keys(content);
          return content;
        }
      }), error => {
        return error;
      });
  }

  postData(url: string, data: any) {
    debugger
    return this.http.post<any>(`${this.appComponent.APIURL}${url}`, data, { withCredentials: false })
      .pipe(map(content => {
        if (!content) {
          if (content.errors.includes('2601')) {
            alert("Same data alredy exist");
            return content;
          }
        }
        else {
          //alert(content.errors[0]);
          return content;
        }
      }), error => {
        return error;
      });
  }

  putData(url: string, id: any, data: any) {
    return this.http.put<any>(`${this.appComponent.APIURL}${url}`, data, { withCredentials: false })
      .pipe(map(content => {
        return content;
      }), error => {
        return error;
      });
  }

  deleteData(url: string, id: any) {
    debugger;
    return this.http.delete<any>(`${this.appComponent.APIURL}${url}`, { withCredentials: false })
      .pipe(map(content => {
        if (!content) {
          if (content.errors.includes('547')) {
            alert("Dependent data");
            return false;
          }
          else {
            alert(content.errors[0]);
            return false;
          }
        }
        return content;
      }), error => {
        return error;
      });
  }

}
