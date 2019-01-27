import { OverlayModule } from '@angular/cdk/overlay';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { DraggableHelperDirective } from './draggable-helper.directive';
import { DraggableDirective } from './draggable.directive';
import { GrippableDirective } from './grippable.directive';
import { SortableListDirective } from './sortable-list.directive';
import { SortableDirective } from './sortable.directive';

@NgModule({
  imports: [CommonModule, OverlayModule],
  declarations: [
    DraggableDirective,
    DraggableHelperDirective,
    SortableDirective,
    SortableListDirective,
    GrippableDirective
  ],
  exports: [
    DraggableDirective,
    DraggableHelperDirective,
    SortableListDirective,
    SortableDirective,
    GrippableDirective
  ],
  providers: [GrippableDirective]
})
export class DraggableModule {}
