import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './views/home/home.component';
import { HeaderComponent } from './Component/header/header.component';
import { MedicineComponent } from './views/medicine/medicine.component';
import { SidebarComponent } from './Component/sidebar/sidebar.component';
import { CreateMedecineComponent } from './views/medicine/create-medecine/create-medecine.component';
import { BillComponent } from './views/bill/bill.component';
import { ApiService } from './services/api.service';
import { CreateBillComponent } from './views/bill/create-bill/create-bill.component';
import { InventoryComponent } from './views/inventory/inventory.component';
import { CreateStockComponent } from './views/inventory/create-stock/create-stock.component';
import { LogInPageComponent } from './Component/log-in-page/log-in-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ForgotPasswordComponent } from './Component/log-in-page/forgot-password/forgot-password.component';
import { CreateBedComponent } from './views/bed-management/BedConfiguration/create-bed/create-bed.component';
import { PatientManagementComponent } from './views/patient-management/patient-management.component';
import { AddPatientComponent } from './views/patient-management/add-patient/add-patient.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { DatePipe } from '@angular/common';
/*import { HotToastModule } from '@ngneat/hot-toast';*/
import { CreateBedNumberComponent } from './views/bed-management/bed-number/create-bed-number/create-bed-number.component';
import { BedTypeComponent } from './views/bed-management/bed-type/bed-type.component';
import { CreateBedTypeComponent } from './views/bed-management/bed-type/create-bed-type/create-bed-type.component';
import { BedManagementComponent } from './views/bed-management/BedConfiguration/bed-management.component';
import { BedNumberComponent } from './views/bed-management/bed-number/bed-number.component';

import { AuthGuard } from './_helpers/auth.guard';
import { ManageRolesComponent } from './views/user management/manage-roles/manage-roles.component';
import { ManageUserComponent } from './views/user management/manage-user/manage-user.component';
import { AddRolesComponent } from './views/user management/manage-roles/add-roles/add-roles.component';
import { AddUserComponent } from './views/user management/manage-user/add-user/add-user.component';
import { DashboardComponent } from './views/dashboard/dashboard.component';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    MedicineComponent,
    SidebarComponent,
    CreateMedecineComponent,
    BillComponent,
    CreateBillComponent,
    InventoryComponent,
    CreateStockComponent,
    LogInPageComponent,

    ForgotPasswordComponent,
    BedManagementComponent,
    CreateBedComponent,
    PatientManagementComponent,
    AddPatientComponent,
    BedNumberComponent,
    CreateBedNumberComponent,
    BedTypeComponent,
    CreateBedTypeComponent,
    ManageRolesComponent,
    ManageUserComponent,
    AddRolesComponent,
    AddUserComponent,
    DashboardComponent,

    
  ],
  imports: [
    /*HotToastModule.forRoot(),*/
    Ng2SearchPipeModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {
        path: '', redirectTo: '/login', pathMatch: 'full'
      },
      { path: 'newpassword', component: ForgotPasswordComponent },

      { path: 'hrms', redirectTo: '/hrms/dashboard', pathMatch: 'full' },
      {
        path: 'hrms', component: HomeComponent,
        canActivate: [AuthGuard],
        children: [
          { path: 'dashboard', component: DashboardComponent },
          { path: 'medicines', component: MedicineComponent },
          { path: 'medicines/add', component: CreateMedecineComponent },
          { path: 'bills', component: BillComponent },
          { path: 'bills/add', component: CreateBillComponent },
          { path: 'inventory', component: InventoryComponent },
          { path: 'inventory/add', component: CreateStockComponent },
          { path: 'bed-management/beds', component: BedManagementComponent },
          { path: 'bed-management/bed-numbers', component: BedNumberComponent },
          { path: 'bed-management/bed-types', component: BedTypeComponent },
          { path: 'patient', component: PatientManagementComponent },
          { path: 'patient/add', component: AddPatientComponent },
           { path: 'roles', component: ManageRolesComponent },
          { path: 'roles/add', component: AddRolesComponent },
          { path: 'user', component: ManageUserComponent },
          { path: 'user/add', component: AddUserComponent },
        ]
      },

      { path: 'login', component: LogInPageComponent },
    ], { useHash: true }),
    BrowserAnimationsModule
  ],
  providers: [ApiService, AppComponent, DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
