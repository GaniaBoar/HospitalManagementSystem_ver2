import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AppComponent } from '../../../app.component';
import { ApiService } from '../../../services/api.service';

import { PatientManagementComponent } from '../patient-management.component';
declare var $: any;

@Component({
  selector: 'app-add-patient',
  templateUrl: './add-patient.component.html',
  styleUrls: ['./add-patient.component.css']
})
export class AddPatientComponent implements OnInit {
 @Input() patient: any;
    form: any;

  constructor(private apiService: ApiService, private router: Router,
    private appComponent: AppComponent, private patientmanagementcomponent: PatientManagementComponent) { }

  ngOnInit() {
    console.log(this.patient)
  }


 
  save() {
    if (this.patient.id > 0) {
      this.update();
    }
    else {
      this.insert();
    }

  }

  private insert() {
    this.apiService.postData('/patientRegistration', this.patient).subscribe((res: any) => {
      if (res) {
        this.appComponent.notify("Success", "Patient saved successfully", 'success');
        window.location.reload();
        $("#patientModal").modal('toggle');
      } else {
        this.appComponent.notify("Error", "Something went wrong. Try again later.", 'error');
      }

    });
  }



  private update() {
    this.apiService.putData('/patientRegistration/' + this.patient.id, this.patient.id, this.patient).subscribe((res: any) => {
      if (res) {
        this.appComponent.notify("Success", "Patient updated.", 'success');
        window.location.reload();
        $("#patientModal").modal('toggle');
      } else {
        this.appComponent.notify("Error", "Something went wrong. Try again later.", 'error');
      }

    });
  }


}

