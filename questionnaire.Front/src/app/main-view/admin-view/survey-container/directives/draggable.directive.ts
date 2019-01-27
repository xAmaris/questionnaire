import {
  AfterContentInit,
  ContentChild,
  Directive,
  ElementRef,
  EventEmitter,
  forwardRef,
  HostBinding,
  HostListener,
  Output
} from '@angular/core';
import { GrippableDirective } from './grippable.directive';

@Directive({
  selector: '[appDraggable]'
})
export class DraggableDirective implements AfterContentInit {
  @HostBinding('class.draggable')
  draggable = true;

  @ContentChild(forwardRef(() => GrippableDirective))
  grippable;

  pointerId?: number;
  mouseDownElement: any;
  // to trigger pointer-events polyfill
  @HostBinding('attr.touch-action')
  touchAction = 'none';

  @Output()
  dragStart = new EventEmitter<PointerEvent>();
  @Output()
  dragMove = new EventEmitter<PointerEvent>();
  @Output()
  dragEnd = new EventEmitter<PointerEvent>();
  // @Output()
  // scroll = new EventEmitter<Event>();

  @HostBinding('class.dragging')
  dragging = false;

  ngAfterContentInit() {}

  constructor(public element: ElementRef) {}

  // @HostListener('window:mousewheel', ['$event'])
  // public onWindowScroll(event: Event): void {
  //   if (this.mouseDownElement) {
  //     // console.log('scrolled');
  //     this.scroll.emit(event);
  //   }
  // }

  @HostListener('pointerdown', ['$event'])
  onPointerDown(event: PointerEvent): void {
    // added after YouTube video: ignore right-click
    if (event.button !== 0) {
      return;
    }
    if (this.grippable.element.nativeElement === event.target) {
      this.mouseDownElement = event.target;
      this.pointerId = event.pointerId;

      this.dragging = true;
      this.dragStart.emit(event);
    }
  }

  @HostListener('document:pointermove', ['$event'])
  onPointerMove(event: PointerEvent): void {
    if (!this.dragging || event.pointerId !== this.pointerId) {
      return;
    }
    this.dragMove.emit(event);
  }

  // added after YouTube video: pointercancel
  @HostListener('document:pointercancel', ['$event'])
  @HostListener('document:pointerup', ['$event'])
  onPointerUp(event: PointerEvent): void {
    if (!this.dragging || event.pointerId !== this.pointerId) {
      return;
    }
    this.mouseDownElement = undefined;
    this.dragging = false;
    this.dragEnd.emit(event);
    window.getSelection().removeAllRanges();
  }
}
