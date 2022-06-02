import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from '../../../app.component';
import { ApiService } from '../../../services/api.service';
declare var $: any;
@Component({
  selector: 'app-create-stock',
  templateUrl: './create-stock.component.html',
  styleUrls: ['./create-stock.component.css']
})
export class CreateStockComponent implements OnInit {
  @Input() stock: any;
  today: any = new Date().toISOString().split('T')[0];
  constructor(private apiService: ApiService, private router: Router, private appComponent: AppComponent) { }

  ngOnInit() {
    console.log(this.stock)
  }
  savestock() {
    if (this.stock.id > 0) {
      this.update();
    }
    else {
      this.insert();
    }

  }

  private insert() {
    this.apiService.postData('/stock', this.stock).subscribe((res: any) => {
      if (res) {
        this.appComponent.notify("Success", "stock saved successfully", 'success');
        window.location.reload();
        $("#inventoryModal").modal('toggle');
      } else {
        this.appComponent.notify("Error", "Something went wrong. Try again later.", 'error');
      }

    });
  }

  private update() {
    this.apiService.putData('/stock/    ' + this.stock.id, this.stock.id, this.stock).subscribe((res: any) => {
      if (res) {
        this.appComponent.notify("Success", "stock updated.", 'success');
        window.location.reload();
        $("#inventorykModal").modal('toggle');
      } else {
        this.appComponent.notify("Error", "Something went wrong. Try again later.", 'error');
      }

    });
  }
}
