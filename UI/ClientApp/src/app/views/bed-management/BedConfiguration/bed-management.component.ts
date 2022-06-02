import { Component, OnInit } from '@angular/core';
import * as _swal from 'sweetalert';
import { SweetAlert } from 'sweetalert/typings/core';
import { AppComponent } from '../../../app.component';
import { ApiService } from '../../../services/api.service';
const swal: SweetAlert = _swal as any;
declare var $: any;

@Component({
  selector: 'app-bed-management',
  templateUrl: './bed-management.component.html',
  styleUrls: ['./bed-management.component.css']
})
export class BedManagementComponent implements OnInit {
  beds: any = [];
  bed: any = {};
  errorMessage: string;

  constructor(private apiService: ApiService, private appComponent: AppComponent) { }

  ngOnInit() {
    //$(document).ready(function () {
    ///*  $('#dataTable').DataTable();*/
    //});
    this.get();
  }

  get() {
    this.apiService.getData(`/Bed`, null)
      .subscribe((res: any) => {
        this.beds = res;


      }, error => {
        this.errorMessage = "Could not load data at this time. Try again later."
      });
  }
  edit(beds) {
    this.beds = beds;
  }
  delete(beds) {
    debugger;
    swal({
      title: "Are you sure?",
      text: "Are you sure that you want to delete this " + beds.name + "?",
      icon: "warning",
      dangerMode: true,
    })
      .then(willDelete => {
        if (willDelete) {
          this.apiService.deleteData(`/Bed/${beds.id}`, beds.id).subscribe((res: any) => {
            if (res) {
              this.appComponent.notify("Success", "beds '" + beds.name + "' delete successfully", 'success')
              this.get();
            }
          }, error => {
            alert("Error deleting, try again after sometime.");
          })
        }
      });

  }


}
