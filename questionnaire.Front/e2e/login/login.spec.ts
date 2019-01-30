import { browser, protractor } from 'protractor';
import { LoginPage } from './login.po';
import { httpPostRequest } from 'e2e/http-request';

// import { browser, by, element } from 'protractor';

// describe('angularjs homepage todo list', () => {
//   it('should add a todo', () => {
//     browser.get('https://angularjs.org');

//     element(by.model('todoList.todoText')).sendKeys(
//       'write first protractor test'
//     );
//     element(by.css('[value="add"]')).click();

//     const todoList = element.all(by.repeater('todo in todoList.todos'));
//     expect(todoList.count()).toEqual(3);
//     expect(todoList.get(2).getText()).toEqual('write first protractor test');

//     // You wrote your first test, cross it off the list
//     todoList
//       .get(2)
//       .element(by.css('input'))
//       .click();
//     const completedAmount = element.all(by.css('.done-true'));
//     expect(completedAmount.count()).toEqual(2);
//   });
// });

const validCredentials: any = {
  // Email: 'gabi97_97@o2.pl',
  Email: 'testingquestionnaire@gmail.com',
  Password: '!A123456a'
};
// function logInToApp() {
//   return httpPostRequest(this.loginUrl, this.validCredentials);
// }
describe('Login page', () => {
  let page: LoginPage;
  beforeEach(() => {
    page = new LoginPage();

    const flow = protractor.promise.controlFlow();
    flow
      .execute(httpPostRequest(this.loginUrl, this.validCredentials))
      .then(() => {
        console.log('all is fine and ok');
      })
      .catch(() => {
        console.log('catched');
        page.redirectToRegisterPage();
        // page.navigateToGoogleLogin();
        // page.registerInApp();
        // page.navigateToGoogleLogin();
      });
  });
  it('should log in to app', () => {});
  // it('should do something', () => {
  //   expect(2).toEqual(2);
  // });
});
