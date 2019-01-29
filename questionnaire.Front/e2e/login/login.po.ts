// tslint:disable-next-line:no-implicit-dependencies
import { browser, by, element } from 'protractor';

export class LoginPage {
  private invalidCredentials = {
    username: 'test',
    password: 'test'
  };
  private validCredentials = {
    username: 'gabi97_97@o2.pl'
  }
}
