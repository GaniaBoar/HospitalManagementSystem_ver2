import { Component, OnInit } from '@angular/core';

import { ApiService } from '../../services/api.service';
import * as _swal from 'sweetalert';
import { SweetAlert } from 'sweetalert/typings/core';
import { AppComponent } from '../../app.component';
import { DatePipe } from '@angular/common';
const swal: SweetAlert = _swal as any;
declare var $: any;

@Component({
  selector: 'app-patient-management',
  templateUrl: './patient-management.component.html',
  styleUrls: ['./patient-management.component.css']
})
export class PatientManagementComponent implements OnInit {
  patients: any = [];
  patient: any = {};
  errorMessage: string;

  constructor(private apiService: ApiService, private appComponent: AppComponent, private dateFormate: DatePipe) { }

  ngOnInit() {
    this.get();
  }

  get() {
    this.apiService.getData(`/patientRegistration`, null)
      .subscribe((res: any) => {

        this.patients = res;

      }, error => {
        this.errorMessage = "Could not load data at this time. Try again later."
      });
  }
  edit(patient) {
    patient.dob = this.dateFormate.transform(patient.dob, 'yyyy-MM-dd');
    this.patient = patient;
  
  }
  delete(patient) {
    debugger;
    swal({
      title: "Are you sure?",
      text: "Are you sure that you want to delete this " + patient.name + "?",
      icon: "warning",
      dangerMode: true,
      buttons: ["No", 'Yes'],
      closeOnClickOutside: false,
      
    })
      .then(willDelete => {
        if (willDelete) {
          this.apiService.deleteData(`/patientRegistration/${patient.id}`, patient.id).subscribe((res: any) => {
            if (res) {
              this.appComponent.notify("Success", "Patient'" + patient.name + "' delete successfully", 'success')
              this.get();
            }
          }, error => {
            alert("Error deleting, try again after sometime.");
          })
        }
      });

  }


}

