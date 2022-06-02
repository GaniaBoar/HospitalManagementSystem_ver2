import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../../app.component';
import { ApiService } from '../../../services/api.service';

@Component({
  selector: 'app-manage-user',
  templateUrl: './manage-user.component.html',
  styleUrls: ['./manage-user.component.css']
})
export class ManageUserComponent implements OnInit {
  user: any = {};
  users:any= [];
    errorMessage: string;
  constructor(private apiService: ApiService, private appComponent: AppComponent, private dateFormate: DatePipe) { }

  ngOnInit() {
    this.get();
  }
get() {
  this.apiService.getData(`/account/users`, null)
    .subscribe((res: any) => {

      this.users = res.data;

    }, error => {
      this.errorMessage = "Could not load data at this time. Try again later."
    });
}
  edit(user) {
    this.user = user;
}
}
