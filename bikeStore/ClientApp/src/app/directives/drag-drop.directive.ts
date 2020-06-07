import { Directive, Output, Input, EventEmitter, HostBinding, HostListener, ElementRef } from '@angular/core';

@Directive({
  selector: '[app-drag-n-drop]'
})
export class DragDropDirective {

  constructor(private el: ElementRef) {
  }

  @Output() onFileDropped = new EventEmitter<any>();

  @HostBinding('style.background-color') private background = '#e8eced';
  //@HostBinding('style.opacity') private opacity = '1';

  // Dragover Event
  @HostListener('dragover', ['$event']) dragOver(event) {
    event.preventDefault();
    event.stopPropagation();
    this.background = '#e2eefd';
  }


  // Dragleave Event
  @HostListener('dragleave', ['$event']) public dragLeave(event) {
    event.preventDefault();
    event.stopPropagation();
    this.background = '#ffffff';
  }

  // Drop Event
  @HostListener('drop', ['$event']) public drop(event) {
    event.preventDefault();
    event.stopPropagation();
    this.background = '#ffffff';
    const files = event.dataTransfer.files;
    if (files.length > 0) {
      this.onFileDropped.emit(files);
    }
  }

}
