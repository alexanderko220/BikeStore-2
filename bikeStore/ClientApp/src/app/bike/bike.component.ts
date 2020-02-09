import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params} from '@angular/router';
import { HttpClient } from '@angular/common/http';

export interface IBike {

}

@Component({
  selector: 'app-bike',
  templateUrl: './bike.component.html',
  styleUrls: ['./bike.component.css']
})
export class BikeComponent implements OnInit {

  constructor(private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit() {
    const $p = this.route.params.subscribe((params: Params) => {
      this.http.get<IBike[]>("api/bikes/bike/" + params.bikeId).subscribe(response => {
        console.log(response);
      })
    })
  }

}
