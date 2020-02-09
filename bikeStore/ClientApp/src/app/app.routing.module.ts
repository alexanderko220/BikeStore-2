import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from "./home/home.component";
import { BikeCategoryComponent } from './bike-category/bike-category.component';
import { BikeComponent } from './bike/bike.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'bikes/category/:catId', component: BikeCategoryComponent },
  { path: 'bike/:bikeId', component: BikeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { 

}
