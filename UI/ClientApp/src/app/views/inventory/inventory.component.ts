import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import * as _swal from 'sweetalert';
import { SweetAlert } from 'sweetalert/typings/core';
import { AppComponent } from '../../app.component';
import { DatePipe } from '@angular/common';
const swal: SweetAlert = _swal as any;
declare var $: any;

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  stocks: any = [];
  stock: any = {};
    errorMessage: string;

  constructor(private apiService: ApiService, private appComponent: AppComponent, private dateFormate: DatePipe) { }

  ngOnInit() {
    this.get();
  }

  get() {
    this.apiService.getData(`/stock`, null)
      .subscribe((res: any) => {
          this.stocks = res;
        

      }, error => {
        this.errorMessage = "Could not load data at this time. Try again later."
      });
  }
  edit(stock) {
    debugger;
    stock.purchaseDate = this.dateFormate.transform(stock.purchaseDate, 'yyyy-MM-dd');
    this.stock = stock;
  }
  delete(stock) {
    debugger;
    swal({
      title: "Are you sure?",
      text: "Are you sure that you want to delete this " + stock.name + "?",
      icon: "warning",
      dangerMode: true,
      buttons: ["No", 'Yes'],
      closeOnClickOutside:false,
    })
      .then(willDelete => {
        if (willDelete) {
          this.apiService.deleteData(`/stock/${stock.id}`, stock.id).subscribe((res: any) => {
            if (res) {
              this.appComponent.notify("Success", "stock '" + stock.name + "' delete successfully", 'success')
              this.get();
            }
          }, error => {
            alert("Error deleting, try again after sometime.");
          })
        }
      });

  }
  }


