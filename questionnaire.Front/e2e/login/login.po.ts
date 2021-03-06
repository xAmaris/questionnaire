// tslint:disable:no-implicit-dependencies
import { Credentials, UserData } from 'e2e/models/credential.model';
import { browser, by, element, protractor } from 'protractor';
import { httpPostRequest } from './../http-request';

export class LoginPage {
  EC = protractor.ExpectedConditions;
  loginUrl = 'auth/login';
  validCredentials: UserData = {
    Email: 'testingquestionnaire@gmail.com',
    Password: '!A123456a',
    Username: 'testingquestionnaire',
    Name: 'test',
    Surname: 'test',
    PhoneNumber: '+48123123123'
  };
  navigateToApp() {
    browser.get('/');
  }
  navigateToGoogleLogin() {
    browser.ignoreSynchronization = true;
    browser.get(
      'https://accounts.google.com/signin/v2/identifier?hl=PL&flowName=GlifWebSignIn&flowEntry=ServiceLogin'
    );
    this.logInToGoogleAccount();
  }
  navigateToMail() {
    browser.get('https://mail.google.com/mail/#inbox');
    this.openMail();
  }
  openMail() {
    const mailTag = browser.element
      .all(by.css('table.F.cf.zt'))
      .get(1)
      .element(by.tagName('tbody'))
      .all(by.cssContainingText('span', 'Ankietyzator - aktywacja konta.'))
      .get(1);

    browser
      .wait(
        this.EC.textToBePresentInElement(
          mailTag,
          'Ankietyzator - aktywacja konta.'
        ),
        50000
      )
      .then(() => {
        mailTag.click();
        const sth = browser.element(
          by.cssContainingText('a', 'link aktywacyjny')
        );
        browser.wait(this.EC.visibilityOf(sth), 5000).then(() => {
          sth.click().then(() => {
            browser.getAllWindowHandles().then(handles => {
              browser.switchTo().window(handles[1]);
              this.navigateToApp();
              this.logInToApp();
            });
          });
        });
      });
  }
  redirectToRegisterPage() {
    browser.get('/auth/register');
    browser.waitForAngular();
    this.registerInApp();
  }
  registerInApp() {
    const label = element(
      by.css(
        'form>div:nth-of-type(1)>div:nth-of-type(1)>div:nth-of-type(1)>div:nth-of-type(2)>label'
      )
    );
    browser.wait(this.EC.visibilityOf(label), 1000).then(() => {
      label.click();
      const firstInput = element(by.xpath('//*[@id="name"]'));

      browser.wait(this.EC.visibilityOf(firstInput), 10000).then(() => {
        firstInput.click();
        firstInput.sendKeys(this.validCredentials.Name);
        this.browserSendKeys(protractor.Key.TAB);
        this.browserSendKeys(this.validCredentials.Surname);
        this.browserSendKeys(protractor.Key.TAB);
        this.browserSendKeys(this.validCredentials.Email);
        this.browserSendKeys(protractor.Key.TAB);
        this.browserSendKeys(this.validCredentials.PhoneNumber);
        this.browserSendKeys(protractor.Key.TAB);
        this.browserSendKeys(this.validCredentials.Password);
        this.browserSendKeys(protractor.Key.TAB);
        this.browserSendKeys(this.validCredentials.Password);
        element(by.css('button>span')).click();
        this.navigateToGoogleLogin();
      });
    });
  }
  browserSendKeys(keys: any) {
    browser.actions().sendKeys(keys).perform();
  }
  logInToGoogleAccount() {
    browser
      .element(by.css('input[name="identifier"]'))
      .sendKeys(this.validCredentials.Username);
    browser
      .element(by.css('input[name="identifier"]'))
      .sendKeys(protractor.Key.ENTER);
    browser
      .wait(
        this.EC.visibilityOf(browser.element(by.css('input[name="password"]'))),
        1000
      )
      .then(() => {
        browser
          .element(by.css('input[name="password"]'))
          .sendKeys(this.validCredentials.Password);
        browser
          .element(by.css('input[name="password"]'))
          .sendKeys(protractor.Key.ENTER);
        browser
          .wait(
            this.EC.textToBePresentInElement(
              element(by.css('h1')),
              'Cześć, test test'
            )
          )
          .then(() => {
            this.navigateToMail();
          });
      });
  }
  logInToApp() {
    const emailinput = browser.element(
      by.css(
        'mat-form-field:nth-of-type(1)>div:nth-of-type(1)>div:nth-of-type(1)>div>input'
      )
    );
    browser.wait(this.EC.visibilityOf(emailinput), 5000).then(() => {
      emailinput.click();
      emailinput.sendKeys(this.validCredentials.Email);
      this.browserSendKeys(protractor.Key.TAB);
      this.browserSendKeys(this.validCredentials.Password);
      const img = browser.element(by.xpath('//*[@id="logo"]/img'));
      browser.wait(this.EC.visibilityOf(img), 50000).then(() => {
        console.log('WORKING......................');
      });
    });
  }
  logInToAppRequest() {
    return httpPostRequest(this.loginUrl, this.validCredentials);
  }
}
