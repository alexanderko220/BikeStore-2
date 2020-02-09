import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { HttpClient } from '@angular/common/http';

export interface IBike {
  bId: number
  bBrand: string
  bModel: string
  bPrice: number
  imgBase64String: string
  bImgId: number
  bSizes: Array<any>
  bColors: Array<any>
}

@Component({
  selector: 'app-bike-category',
  templateUrl: './bike-category.component.html',
  styleUrls: ['./bike-category.component.css']
})
export class BikeCategoryComponent implements OnInit {

  constructor(private route: ActivatedRoute, private http: HttpClient) { }
  public bikes: IBike[] = [];
  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.http.get<IBike[]>("api/bikes/category/" + params.catId).subscribe(response => {
        console.log(response);
        this.bikes = response;
      })
    })
  }

}
