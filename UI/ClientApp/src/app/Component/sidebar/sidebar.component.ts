import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  role: string;
    user: any;

  constructor(private authService: AuthenticationService) { }

  ngOnInit() {
    this.user = this.authService.userValue;
    this.role = "SuperAdmin"; //this.authService.userValue.roles[0];
  }

}
