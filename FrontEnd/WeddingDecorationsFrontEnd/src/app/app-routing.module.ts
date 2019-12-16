import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AdminAuthenticationGuard } from './shared/services/admin-services/_helper-services/_guard/admin.authentication.guard';

import { MainComponent } from './main/main.component';
import { AdminComponent } from "./admin-page/admin-login/admin.component";
import { BookingComponent } from "./booking/booking.component";
import { AdminMainComponent } from "./admin-page/admin-main/admin-main-page.component";


const routes: Routes = [
  { path: '', component: MainComponent },
  { path: 'admin', component: AdminComponent},
  { path: 'admin-main', component: AdminMainComponent, canActivate: [AdminAuthenticationGuard]},
  { path: 'booking', component: BookingComponent}
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes,
    {
      anchorScrolling: 'enabled',
      onSameUrlNavigation: 'reload',
      scrollPositionRestoration: 'enabled'
    })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
