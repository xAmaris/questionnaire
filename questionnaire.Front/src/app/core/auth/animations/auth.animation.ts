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
const query = (s: any, a: any, o = { optional: true }) => q(s, a, o);

export const basicTransition = trigger('basicTransition', [
  transition('* <=> *', [
    query(':enter, :leave', style({ position: 'absolute', width: '100%' })),
    query(
      ':enter',
      style({
        transform: 'translateX(10%)'
      })
    ),
    sequence([
      group([
        query(':enter', [
          style({
            transform: 'translateX(100%)',
            opacity: 0
          }),
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
