// tslint:disable:max-classes-per-file

export class UserProfile {
  firstName: string;
  lastName: string;
  email: string;
  phoneNum: string;
}
export class User extends UserProfile {
  password: string;
  profileName: string;
}
export class Graduate extends User {}
export class Student extends User {
  albumID: string;
}
export class Employer extends User {
  companyName: string;
  location: string;
  companyDescription: string;
}
export class ProfileDataStorage {
  name: string;
  surname: string;
  role: string;
  phoneNumber: string;
  email: string;
  loginData: LoginData;
}
export class LoginData {
  token: string;
}
