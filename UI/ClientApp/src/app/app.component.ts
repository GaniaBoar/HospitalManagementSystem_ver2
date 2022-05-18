import { Component } from '@angular/core';
import * as _swal from 'sweetalert';
import { SweetAlert } from 'sweetalert/typings/core';

const swal: SweetAlert = _swal as any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  APIURL = window["APIURL"];
  constructor() { }

  ngOnInit() {
  }

  notify(title, message, icon) {
    swal(title, message, icon);
  }
}
