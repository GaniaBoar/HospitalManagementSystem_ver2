import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from '../../../../app.component';
import { ApiService } from '../../../../services/api.service';
declare var $: any;
@Component({
  selector: 'app-add-roles',
  templateUrl: './add-roles.component.html',
  styleUrls: ['./add-roles.component.css']
})
export class AddRolesComponent implements OnInit {
  @Input() role: any;
  constructor(private appComponent: AppComponent, private apiService: ApiService, private router: Router) { }

  ngOnInit() {
  }

  save() {
    debugger;
    if (this.role.id != undefined) {
      this.update();
    }
    else {
      this.insert();
    }

  }

  private insert() {
    debugger;
    this.apiService.postData('/account/roles/create', this.role).subscribe((res: any) => {
      if (!res.hasError) {
        this.appComponent.notify("Success", "Role saved successfully", 'success');
        window.location.reload();
        $("#roleModal").modal('toggle');
      } else {
        this.appComponent.notify("Error", res.message, 'error');
      }

    });
  }

  private update() {
    debugger;
    this.apiService.putData('/account/roles/edit/' + this.role.id, this.role.id, this.role).subscribe((res: any) => {
      if (!res.hasError) {
        this.appComponent.notify("Success", "Role updated.", 'success');
        this.router.navigate(['hrms/roles']);
        $("#roleModal").modal('toggle');
      } else {
        this.appComponent.notify("Error", res.message, 'error');
      }

    });
  }
}
