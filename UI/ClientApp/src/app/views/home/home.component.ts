import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
 name:any;
 select:any = '';
 quantity:any;
 price:any;
 
  constructor() { }

  ngOnInit() {
  }
save(){ 
  var formdetails={
name: this.name,
select: this.select,
quantity: this.quantity,
price: this.price,
}
console.log(formdetails);
this.clear();
}
clear(){
  this.name='';
  this.select='';
  this.quantity='';
  this.price='';

}
}


