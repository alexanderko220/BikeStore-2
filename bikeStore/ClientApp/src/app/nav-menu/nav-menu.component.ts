import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';



export interface ICategory {
  catId: number;
  mainCatId?: number;
  catName: string;
  catDescr: string;
}

export interface IMenuFlags {
  isBikesHover: boolean;
  isAboutHover: boolean;
  isServicesHover: boolean;
  isContactHover: boolean;

}

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})


export class NavMenuComponent implements OnInit {

  //#region Params

  public categories: ICategory[] = [];
  public mainCatList: ICategory[] = [];
  
  public flags: IMenuFlags = {
    isBikesHover: false,
    isAboutHover: false,
    isServicesHover: false,
    isContactHover: false
  };

  //#endregion Params
  constructor(private http: HttpClient) { }

  @ViewChild('container', { read: ViewContainerRef, static: false })
  container: ViewContainerRef;

  onHover(propName: string, value: boolean): void {
    for (var key in this.flags) {
      if (key == propName) this.flags[key] = value;
      else this.flags[key] = false;
    }
  }
  closeAllNavs(): void {
    for (var key in this.flags) {
      this.flags[key] = false;
    }
  }
  //#region Init

  ngOnInit() {

    this.http.get<ICategory[]>("api/category").subscribe(response => {
      this.categories = response;
      this.mainCatList = this.categories.filter(element => element.mainCatId === null);

    });
  };

  //#endregion Init
}
