import { Component, Input, OnInit } from '@angular/core';
import { ApiService } from '../../../services/api.service';

@Component({
  selector: 'app-create-bill',
  templateUrl: './create-bill.component.html',
  styleUrls: ['./create-bill.component.css']
})
export class CreateBillComponent implements OnInit {
  Bill: any = {};
  @Input() Content: any;
  constructor(private apiService: ApiService) { }

  ngOnInit() {
   
  }
  checkCaller() {
    if (this.Content.Caller=="POST") {
      this.save();
    }
    else {
      this.edit();
    }
  }

  edit() {
    this.apiService.putData('url', this.Content.Bill.id, this.Content.Bill).subscribe(res => {
      //show success message and haldle error

    })
    }
    save() {
      //this.apiService.postData('MedicineController', this.Content.Bill).subscribe(res => {
      //  //show success message and haldle error

      //})
    }

}
