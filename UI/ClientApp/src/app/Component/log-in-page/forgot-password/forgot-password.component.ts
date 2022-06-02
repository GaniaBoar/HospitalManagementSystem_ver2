import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../services/api.service';

@Component({ 
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  confirmPassword: any;
  newPassword: any;
  constructor() { }
  ngOnInit() {
  }

}
