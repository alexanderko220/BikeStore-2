import { Component, OnInit, Input } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'bike-menu',
  //templateUrl: './bike-menu.component.html',
    template: `<div [innerHTML]="html"></div>`,
  styleUrls: ['./bike-menu.component.css']
})
export class BikeMenuComponent implements OnInit {

  constructor() { }
  @Input() html : SafeHtml;
  ngOnInit() {
  }

}
