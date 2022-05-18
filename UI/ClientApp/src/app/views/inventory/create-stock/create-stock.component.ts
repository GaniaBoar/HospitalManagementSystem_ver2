import { Component, OnInit } from '@angular/core';
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
  inventory: any = {};
  today: any = new Date().toISOString().split('T')[0];
  constructor(private apiService: ApiService, private router: Router, private appComponent: AppComponent) { }

  ngOnInit() {
  }
  saveInventory() {
    console.log(this.inventory)
    this.apiService.postData('/stock', this.inventory).subscribe((res: any) => {
      if (res) {
        this.appComponent.notify("Success", "Stock saved successfully", 'success');
        this.inventory = {};
        $("#inventoryModal").modal('toggle');
      } else {
        this.appComponent.notify("Error", "Something went wrong. Try again later.", 'error');
      }

    })
  }
}
