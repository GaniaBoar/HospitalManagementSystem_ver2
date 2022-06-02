import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-bill',
  templateUrl: './bill.component.html',
  styleUrls: ['./bill.component.css']
})
export class BillComponent implements OnInit {
  public Content:any = {
    Title: "Add Bill",
    Caller:"POST",
    Bills: [
      {id:1,name:"test"}
    ],
    Bill: {}
  }

  constructor() { }

  ngOnInit() {
  }
  create() {
    this.Content.Caller = "POST";
    this.Content.Bill = {};
  }
  edit(bills) {
    this.Content.Title = "Edit Bill";
    this.Content.Caller = "PUT";
    this.Content.Bill = bills;
  }
}
