import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { IsLoadingService } from '@service-work/is-loading';

export interface IBike {
  bikeId: number
  brand: string
  model: string
  price: string
  thumbBase64: string
  imgId: number
  sizes: Array<any>
  colors: Array<any>
}

@Component({
  selector: 'app-bike-category',
  templateUrl: './bike-category.component.html',
  styleUrls: ['./bike-category.component.css']
})

export class BikeCategoryComponent implements OnInit {

  constructor(private route: ActivatedRoute, private http: HttpClient, private isLoadingService: IsLoadingService) { }
  public bikes: IBike[] = [];
  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {

     let promise =  this.http.get<IBike[]>("api/bikes/category/" + params.catId).subscribe(response => {
        console.log(response);
        this.bikes = response;
      })

      this.isLoadingService.add(promise);

      
    })
  }

}
