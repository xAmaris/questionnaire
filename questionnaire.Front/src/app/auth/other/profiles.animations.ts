import {
  trigger,
  stagger,
  animate,
  style,
  query as q,
  transition
} from '@angular/animations';
const query = (s, a, o = { optional: true }) => q(s, a, o);

export const profilesTransition = trigger('profilesTransition', [
  transition('* =>', [
    // query('.card-cc', style({ opacity: 0 })),
    // query(
    //   '.card-cc',
    //   stagger(150, [
    //     style({ transform: 'translateY(100px)' }),
    //     animate(
    //       '300ms cubic-bezier(.75,-0.48,.26,1.52)',
    //       style({
    //         transform: 'translateY(0px)',
    //         opacity: 1
    //       })
    //     )
    //   ])
    // )
  ]),
  transition(':leave', [
    // query('.card-cc', [
    //   style({
    //     transform: 'translateY(0%)',
    //     opacity: 1,
    //   }),
    //   animate(
    //     '0.5s cubic-bezier(.75,-0.48,.26,1.52)',
    //     style({
    //       transform: 'translateY(100px)',
    //       opacity: 0
    //     })
    //   )
    // ])
  ])
]);

// style({ transform: 'translateY(0px)', opacity: 1 }),
// animate(
//   's cubic-bezier(.75,-0.48,.26,1.52)',
//   style({ transform: 'translateY(100px)', opacity: 0 })
// )
export const trigtrans = trigger('trigtrans', [
  transition(':enter', [
    style({ height: 0, overflow: 'hidden' , 'max-height': '*'}),
    animate('.3s ease', style({ height: '600px', 'background-color': 'red' }))
  ]),
  transition(':leave', [
    style({ height: '*', overflow: 'hidden' }),
    animate('.3s ease', style({ height: 0 }))
  ])
]);
