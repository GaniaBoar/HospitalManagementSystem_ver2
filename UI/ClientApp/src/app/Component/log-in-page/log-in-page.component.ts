import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-log-in-page',
  templateUrl: './log-in-page.component.html',
  styleUrls: ['./log-in-page.component.css']
})
export class LogInPageComponent implements OnInit {
  public Content: any;
  isLoggingIn: boolean = false;
  constructor(private router: Router, private authenticationService: AuthenticationService) { }

  ngOnInit() {
  }
  login(userName, password) {
    this.isLoggingIn = true;
    debugger
    if (userName == 'admin' && password == 'Admin123') {
      this.router.navigate(['hrms'])
    }
    else {
      alert('Username and Password incorrect.')
    }
  }

  //login(userName, password) {
  //  this.isLoggingIn = true;
  //  this.authenticationService.login(userName, password)
  //    .subscribe((res: any) => {
  //      debugger;
  //      this.isLoggingIn = false;
  //      if (!res) {
  //        //this.toast.error("Invalid login attempt. Please try again", {
  //        //  position: 'top-right',
  //        //  autoClose: true
  //        //});
  //        alert("Invalid login attempt. Please try again")
  //      }
  //      else {
  //        this.router.navigate(['hrms']);
  //      }

  //    }, error => {
  //      this.isLoggingIn = false;
  //      //this.toast.error("Invalid login attempt. Please try again", {
  //      //  position: 'top-right',
  //      //  autoClose: true

  //      //});
  //      return false;
  //    })
  //}
}
