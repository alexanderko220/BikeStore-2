import { Directive, ViewContainerRef, Renderer2, ElementRef } from '@angular/core';

@Directive({
    selector: '[menuRef]'
})

export class RefDirective {
  constructor(public containerRef: ViewContainerRef, public elem: ElementRef,public render: Renderer2) {

  }
}
