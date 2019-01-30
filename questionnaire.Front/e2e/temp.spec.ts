// import { browser, protractor } from 'protractor';
// import { Credentials } from './models/credential.model';

// // import { browser, by, element } from 'protractor';

// // describe('angularjs homepage todo list', () => {
// //   it('should add a todo', () => {
// //     browser.get('https://angularjs.org');

// //     element(by.model('todoList.todoText')).sendKeys(
// //       'write first protractor test'
// //     );
// //     element(by.css('[value="add"]')).click();

// //     const todoList = element.all(by.repeater('todo in todoList.todos'));
// //     expect(todoList.count()).toEqual(3);
// //     expect(todoList.get(2).getText()).toEqual('write first protractor test');

// //     // You wrote your first test, cross it off the list
// //     todoList
// //       .get(2)
// //       .element(by.css('input'))
// //       .click();
// //     const completedAmount = element.all(by.css('.done-true'));
// //     expect(completedAmount.count()).toEqual(2);
// //   });
// // });

// const headersOpt = {
//   header: {
//     'Content-type': 'application/json'
//   }
// };

// // tslint:disable:no-implicit-dependencies
// // tslint:disable:no-var-requires
// const request = require('request');
// const validCredentials: any = {
//   // Email: 'gabi97_97@o2.pl',
//   Email: 'testingquestionnaire@gmail.pl',
//   Password: '!A123456a'
// };
// describe('Sample test', () => {
//   beforeEach(() => {
//     browser.get('/');
//     const jar = request.jar();
//     const req = request.defaults({
//       jar
//     });

//     function post(url: string, params: any) {
//       const defer = protractor.promise.defer();
//       console.log('Calling', url, params);
//       req.post(
//         {
//           url,
//           body: params,
//           json: true
//         },
//         (error: any, message: any) => {
//           console.log('Done call to', url);
//           if (error || message.statusCode >= 400) {
//             console.log('error:', error);
//             console.log(message.statusCode);
//             defer.reject({
//               error,
//               message
//             });
//           } else {
//             defer.fulfill(message);
//           }
//         }
//       );
//       return defer.promise;
//     }

//     function setupCommon() {
//       return post('http://localhost:5000/api/auth/login', validCredentials);
//     }

//     const flow = protractor.promise.controlFlow();
//     flow
//       .execute(setupCommon)
//       .then(() => {
//         console.log('all is fine and ok');
//       })
//       .catch(() => {
//         console.log('catched');
//         browser.get(
//           'https://accounts.google.com/signin/v2/identifier?hl=PL&flowName=GlifWebSignIn&flowEntry=ServiceLogin'
//         );
//       });
//   });

//   it('should do something', () => {
//     expect(2).toEqual(2);
//   });
// });
