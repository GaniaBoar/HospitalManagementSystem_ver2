import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from '../../../../app.component';
import { ApiService } from '../../../../services/api.service';
import { BedManagementComponent } from '../bed-management.component';
declare var $: any;
@Component({
  selector: 'app-create-bed',
  templateUrl: './create-bed.component.html',
  styleUrls: ['./create-bed.component.css']
})
export class CreateBedComponent implements OnInit {
  @Input() beds: any;
  bedTypes: any = [
    {
      "id": 1,
      "name": "AC Private",
    },
    {
      "id": 2,
      "name": "General Ward",
    },
    {
      "id": 3,
      "name": "ICU",
    }
  ];
  errorMessage: string;
  today: any = new Date().toISOString().split('T')[0];
    showallocatedTo: boolean;
    allocation_errorMessage: boolean;
  constructor(private apiService: ApiService, private router: Router,
    private appComponent: AppComponent, private bedmanagementcomponent: BedManagementComponent) { }

  ngOnInit() {
    console.log(this.beds);
   // this.getBedTypes(); uncomment when Api is ready
  }

  // uncomment when Api is ready
  //getBedTypes() {
  //  this.apiService.getData(`/BedTypes`, null)
  //    .subscribe((res: any) => {
  //      this.bedTypes = res;


  //    }, error => {
  //      this.errorMessage = "Could not load data at this time. Try again later."
  //    });
  //}

  calculateTotal(event: any){

  }
  checkallocatedFrom(allocatedTo) {
    if (allocatedTo <= this.beds.allocatedFrom) {
      this.allocation_errorMessage = true;
      this.beds.allocatedTo = null;
    }
    else {
      this.allocation_errorMessage = false;
    }
  }

  diagnosticChangeCharges(event: any) {
    let value = event.target.value
    if (value == "ac") {
      this.beds.id.charges = 3000;
    }
    if (value == "general") {
      this.beds.id.charges = 300;

    }
    if (value == "icu") {
      this.beds.id.charges = 5000;
    }
  }
  save() {
    if (this.beds.id > 0) {
      this.update();
    }
    else {
      this.insert();
    }

  }

  private insert() {
    this.apiService.postData('/Bed', this.beds).subscribe((res: any) => {
      if (res) {
        this.appComponent.notify("Success", "Bed saved successfully", 'success');
        this.beds = {};
        this.router.navigate(['hrms/Bed']);
        $("#bedsModal").modal('toggle');
      } else {
        this.appComponent.notify("Error", "Something went wrong. Try again later.", 'error');
      }

    });
  }

  private update() {
    this.apiService.putData('/Bed/' + this.beds.id, this.beds.id, this.beds).subscribe((res: any) => {
      if (res) {
        this.appComponent.notify("Success", "Medicine updated.", 'success');
        this.beds = {};
        this.router.navigate(['hrms/Bed']);
        $("#bedsModal").modal('toggle');
      } else {
        this.appComponent.notify("Error", "Something went wrong. Try again later.", 'error');
      }

    });
  }
}

