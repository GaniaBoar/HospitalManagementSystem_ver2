import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import * as _swal from 'sweetalert';
import { SweetAlert } from 'sweetalert/typings/core';
import { AppComponent } from '../../../app.component';
import { ApiService } from '../../../services/api.service';
const swal: SweetAlert = _swal as any;
@Component({
  selector: 'app-manage-roles',
  templateUrl: './manage-roles.component.html',
  styleUrls: ['./manage-roles.component.css']
})
export class ManageRolesComponent implements OnInit {
  roles: any = [];
  role: any = {};
    errorMessage: string;
  constructor(private apiService: ApiService, private appComponent: AppComponent, private dateFormate: DatePipe) { }

  ngOnInit() {
    this.get();
  }
  get() {
    this.apiService.getData(`/account/roles`, null)
      .subscribe((res: any) => {

        this.roles = res.data;

      }, error => {
        this.errorMessage = "Could not load data at this time. Try again later."
      });
  }
  edit(_role) {
    this.role = _role;
  }
  //delete(_role) {
  //  debugger;
  //  swal({
  //    title: "Are you sure?",
  //    text: "Are you sure that you want to delete this " + _role.name + "?",
  //    icon: "warning",
  //    dangerMode: true,
  //  })
  //    .then(willDelete => {
  //      if (willDelete) {
  //        this.apiService.deleteData(`/Account/roles/delete/${_role.id}`, _role.id).subscribe((res: any) => {
  //          if (res) {
  //            this.appComponent.notify("Success", "Role '" + _role.name + "' delete successfully", 'success')
  //            this.get();
  //          }
  //        }, error => {
  //          alert("Error deleting, try again after sometime.");
  //        })
  //      }
  //    });

  //}
} 
