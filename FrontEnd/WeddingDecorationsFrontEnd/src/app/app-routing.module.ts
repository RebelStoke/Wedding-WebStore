import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main/main.component';
import {AdminComponent} from "./admin/admin.component";
import {BookingComponent} from "./booking/booking.component";


const routes: Routes = [
  { path: '', component: MainComponent },
  { path: 'admin', component: AdminComponent},
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
