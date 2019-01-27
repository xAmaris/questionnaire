import {
  AfterContentInit,
  Directive,
} from '@angular/core';

@Directive({
  selector: '[appGrippable]'
})
export class GrippableDirective implements AfterContentInit {
  ngAfterContentInit() {
  }

  constructor() {
  }
}
