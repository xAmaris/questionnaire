import { browser, by, protractor } from 'protractor';
import { httpPostRequest } from './../http-request';
import { LoginPage } from './login.po';

const page = new LoginPage();
function logInToApp() {
  return httpPostRequest(page.loginUrl, page.validCredentials);
}
describe('Login page', () => {
  beforeEach(() => {
    const flow = protractor.promise.controlFlow();
    flow
      .execute(logInToApp)
      .then(async () => {
        page.navigateToApp();
        await page.logInToApp();
        console.log('all is fine and ok');
      })
      .catch(() => {
        page.redirectToRegisterPage();
      });
  });
  it('should log in to app', () => {
    expect(
      browser.element(by.xpath('//*[@id="logo"]/img')).isDisplayed()
    ).toBeTruthy();
  });
  // it('should do something', () => {
  //   expect(2).toEqual(2);
  // });
});
