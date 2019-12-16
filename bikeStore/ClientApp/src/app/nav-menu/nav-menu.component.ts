import { Component, OnInit, ViewChild, ViewContainerRef, ComponentFactoryResolver} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { BikeMenuComponent } from '../bike-menu/bike-menu.component';
import { RefDirective } from "../directives/ref.directive";
//import { MatMenu } from '@angular/material/menu';


export interface ICategory {
  catId: number;
  mainCatId?: number;
  catName: string;
  catDescr: string;
}

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})


export class NavMenuComponent implements OnInit {

    public categories: ICategory[] = [];

    constructor(private http: HttpClient, private resolver: ComponentFactoryResolver, private sanitizer: DomSanitizer) { }

    @ViewChild('container', { read: ViewContainerRef , static: false})
    container: ViewContainerRef;
    @ViewChild(RefDirective, { static: true }) refDir: RefDirective;

    createMainMenu(nodeList: ICategory[]): string {
      
        let html: string = `<div class="row">`;
        
        let firstLayer = nodeList.filter(element => element.mainCatId === null);
        
        for (let i = 0; i < firstLayer.length; i++) {
            html += `<div id="item${firstLayer[i].catId}" class="col-lg-3 mb-4">
                      <h6 class="text-white font-weight-bold text-uppercase">${firstLayer[i].catName}</h6>`;
            html += this.buildList(nodeList, firstLayer[i].catId);
            html += `</div>`;
        }
       
        return html += `</div>`;
    }

    buildList(nodeList: ICategory[], currentId: number): string {
        let subMenuList = nodeList.filter(item => item.mainCatId === currentId);
        let html: string = subMenuList.length > 0 ? `<ul class="list-unstyled" id="item${subMenuList[0].mainCatId}" >` : ``;
        for (let i = 0; i < subMenuList.length; i++) {

            html += `<li class="nav-item"> <a href="" class="nav-link text-small pb-0" >${subMenuList[i].catName}</a></li>`;
        }
        html += subMenuList.length > 0 ? `</ul>` : ``;
        return html;
    }

    ngOnInit() {
      
         this.http.get<ICategory[]>("api/category").subscribe(response => {
             this.categories = response;
             const modalFactory = this.resolver.resolveComponentFactory(BikeMenuComponent);
             //this.refDir.containerRef.clear();
             const component = this.refDir.containerRef.createComponent(modalFactory);
             component.instance.html = this.sanitizer.bypassSecurityTrustHtml(this.createMainMenu(this.categories));
             
         });
    }
}
