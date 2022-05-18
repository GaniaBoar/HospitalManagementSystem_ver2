import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from '../../../app.component';
import { ApiService } from '../../../services/api.service';

import { MedicineComponent } from '../medicine.component';
declare var $: any;
@Component({
  selector: 'app-create-medecine',
  templateUrl: './create-medecine.component.html',
  styleUrls: ['./create-medecine.component.css']
})
export class CreateMedecineComponent implements OnInit {
  @Input() medicine;
  showExpError: boolean = false;
  today: any = new Date().toISOString().split('T')[0];
  constructor(private apiService: ApiService, private router: Router,
    private appComponent: AppComponent, private medicineComponent: MedicineComponent) { }
 
  ngOnInit() {
    console.log(this.medicine)
  }

  checkMfdDate(expDate) {
    if (expDate <= this.medicine.mfddate) {
      this.showExpError = true;
      this.medicine.expdate = null;
    }
    else {
        this.showExpError = false;
      }


  }
  save() {
    if (this.medicine.id > 0) {
      this.update();
    }
    else {
      this.insert();
    }
  
  }

  private insert() {
    this.apiService.postData('/medicine', this.medicine).subscribe((res: any) => {
            if (res) {
                this.appComponent.notify("Success", "Medicine saved successfully", 'success');
                this.router.navigate(['hrms/medicines']);
                $("#medicineModal").modal('toggle');
            } else {
                this.appComponent.notify("Error", "Something went wrong. Try again later.", 'error');
            }

        });
    }

  private update() {
    this.apiService.putData('/medicine/' + this.medicine.id, this.medicine.id, this.medicine).subscribe((res: any) => {
            if (res) {
                this.appComponent.notify("Success", "Medicine updated.", 'success');
                this.router.navigate(['hrms/medicines']);
                $("#medicineModal").modal('toggle');
            } else {
                this.appComponent.notify("Error", "Something went wrong. Try again later.", 'error');
            }

        });
    }
}
