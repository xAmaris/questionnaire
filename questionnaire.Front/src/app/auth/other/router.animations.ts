import {
  animate,
  animateChild,
  group,
  query as q,
  sequence,
  style,
  transition,
  trigger
} from '@angular/animations';
const query = (s, a, o = { optional: true }) => q(s, a, o);

export const routerTransition = trigger('routerTransition', [
  transition('* => *', [
    query(':enter, :leave', style({ position: 'fixed', width: '100%' })),
    query(':enter', style({ transform: 'translateX(100%)' })),
    sequence([
      query(':leave', animateChild()),
      group([
        query(':leave', [
          style({ transform: 'translateX(0%)' }),
          animate(
            '500ms cubic-bezier(.75,-0.48,.26,1.52)',
            style({ transform: 'translateX(-100%)' })
          )
        ]),
        query(':enter', [
          style({ transform: 'translateX(100%)' }),
          animate(
            '500ms cubic-bezier(.75,-0.48,.26,1.52)',
            style({ transform: 'translateX(0%)' })
          )
        ])
      ]),
      query(':enter', animateChild())
    ])
  ])
]);

export const basicTransition = trigger('basicTransition', [
  transition('* <=> *', [
    query(':enter, :leave', style({ position: 'absolute', width: '100%' })),
    query(
      ':enter',
      style({
        transform: 'translateX(10%)'
      })
    ),
    // query('#centered-container', style({'height': '400px'})),
    sequence([
      group([
        // query(':self', [
        //   style({ height: '0' }),
        //   animate(
        //     '0.5s ease',
        //     style({ height: '0', })
        //   )
        // ]),
        // query(':host', [
        //   style({ height: '*' }),
        //   animate(
        //     '1.5s ease',
        //     style({ height: '0', 'background-color': 'red' })
        //   )
        // ]),
        query(':enter', [
          style({
            transform: 'translateX(100%)',
            opacity: 0
            // height: 0
          }),
          // animate('0.1s ease', style({ height: '600px', })),
          animate(
            '0.5s ease-in-out',
            style({
              transform: 'translateX(0%)',
              opacity: 1
            })
          ),
          animate('0.5s ease', style({ opacity: 1 }))
        ]),
        query(':enter', animateChild()),

        query(':leave', [
          style({
            transform: 'translateX(0%)',
            opacity: 1
          }),
          animate(
            '0.5s ease-in-out',
            style({
              transform: 'translateX(-100%)',
              opacity: 0
            })
          )
        ]),
        query(':self', [
          style({ height: '*' }),
          animate('0.5s ease', style({ height: '*' }))
        ]),
         query(':leave', animateChild())
      ])
    ])
  ])
]);
