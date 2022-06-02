import { Component, Input, OnInit } from '@angular/core';
/*import { FormControl, FormGroup, Validators } from '@angular/forms';*/
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AppComponent } from '../../../../app.component';
import { ApiService } from '../../../../services/api.service';
declare var $: any;
@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;
  passwordMatched: boolean = true;
  roles: any = [];
  @Input() user: any;
    passwordPattern: boolean=true;
 
  constructor(private appComponent: AppComponent, private formBuilder: FormBuilder, private apiService: ApiService, private router: Router) { }

  ngOnInit() {

    this.getRoles();
  }

  getRoles() {
    this.apiService.getData("/account/roles", null).subscribe(res => {
      this.roles = res.data.filter(a => a.name != 'SuperAdmin');

    })
    


  }


  checkPassword() {
    debugger
    let regx = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,30}$/;
    if (!regx.test(this.user.password)) {
      this.passwordPattern = false;
    }
    else {
      this.passwordPattern = true;
    }
    if (this.user.confirmpassword != 'undefined' && (this.user.password != this.user.confirmpassword)) {
      this.passwordMatched = false;
    }
    else {
      this.passwordMatched = true;
    }
  }

  save() {
    this.checkPassword();
    if (this.user.id != undefined) {
      this.update();
    }
    else {
      this.insert();
    }

  }

  private insert() {
    debugger
    this.apiService.postData('/account/signup', this.user).subscribe((res: any) => {
      if (!res.hasError) {
        this.appComponent.notify("Success", "User saved successfully", 'success');
        window.location.reload();
        $("#userModel").modal('toggle');
      } else if (res.errors.length > 0) {
        this.appComponent.notify("Error", res.errors.toString(), 'error');
      } else {
        this.appComponent.notify("Error", res.message, 'error');
      }

    });
  }

  private update() {
    debugger;
    this.apiService.putData('/account/users/' + this.user.id, this.user.id, this.user).subscribe((res: any) => {
      if (!res.hasError) {
        this.appComponent.notify("Success", "User updated.", 'success');
        window.location.reload();
        $("#userModel").modal('toggle');
      } else if (res.errors.length > 0) {
        this.appComponent.notify("Error", res.errors.toString(), 'error');
      }
      else
        {
        this.appComponent.notify("Error", res.message, 'error');
      }

    });
  }
}
