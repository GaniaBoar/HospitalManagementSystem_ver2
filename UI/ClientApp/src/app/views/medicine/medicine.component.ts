import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import * as _swal from 'sweetalert';
import { SweetAlert } from 'sweetalert/typings/core';
import { AppComponent } from '../../app.component';
import { DatePipe } from '@angular/common';
const swal: SweetAlert = _swal as any;
declare var $: any;

@Component({
  selector: 'app-medicine',
  templateUrl: './medicine.component.html',
  styleUrls: ['./medicine.component.css']
})
export class MedicineComponent implements OnInit {
  medicines: any = [];
  medicine: any = {};
  errorMessage: string;

  constructor(private apiService: ApiService, private appComponent: AppComponent, private dateFormate: DatePipe) { }

  ngOnInit() {
    this.get();
  }

  get() {
    this.apiService.getData(`/medicine`, null)
      .subscribe((res: any) => {
        
          this.medicines = res;
        
      }, error => {
        this.errorMessage = "Could not load data at this time. Try again later."
      });
  }

  edit(medicine) {
    debugger;
    medicine.mfddate = this.dateFormate.transform(medicine.mfddate, 'yyyy-MM-dd');
    medicine.expdate = this.dateFormate.transform(medicine.expdate, 'yyyy-MM-dd');
    this.medicine = medicine;
  }
  delete(medicine) {
    debugger;
    swal({
      title: "Are you sure?",
      text: "Are you sure that you want to delete this " + medicine.name + "?",
      icon: "warning",
      dangerMode: true,
      buttons: ["No", 'Yes'],
      closeOnClickOutside: false,
    })
      .then(willDelete => {
        if (willDelete) {
          this.apiService.deleteData(`/medicine/${medicine.id}`, medicine.id).subscribe((res: any) => {
            if (res) {
              this.appComponent.notify("Success", "Medicine '" + medicine.name + "' delete successfully", 'success')
              this.get();
            }
          }, error => {
            alert("Error deleting, try again after sometime.");
          })
        }
      });
   
  }

 
}
