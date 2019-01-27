// tslint:disable:max-classes-per-file
// tslint:disable:no-duplicate-imports
import { Moment } from 'moment';
import * as _moment from 'moment';

export class User {
  id?: number;
  name: string;
  surname: string;
  email: string;
  constructor(name: string, surname: string, email: string, id?: number) {
    this.id = id;
    this.name = name;
    this.surname = surname;
    this.email = email;
  }
}

export class RegisteredUser extends User {
  albumID: number;
  profileName: string;
  password: string;
}

export class UnregisteredUser extends User {
  course: string;
  dateOfCompletion?: Moment | string;
  typeOfStudy: string;
  constructor(
    name: string,
    surname: string,
    email: string,
    course: string,
    typeOfStudy: string,
    dateOfCompletion?: Moment | string
  ) {
    super(name, surname, email);
    this.course = course;
    this.typeOfStudy = typeOfStudy;
    this.dateOfCompletion = dateOfCompletion;
  }
}

export class UnregisteredUserModel extends UnregisteredUser {
  // course: string;
  completionDate: string;
  // typeOfStudy: string;
  constructor(user: UnregisteredUser) {
    super(user.name, user.surname, user.email, user.course, user.typeOfStudy);
    const date: string = this.isDataString(user.dateOfCompletion)
      ? (user.dateOfCompletion as string)
      : _moment((user.dateOfCompletion as Moment).toISOString())['_i'];
    const created_date: string = date
      .split('T')[0]
      .split('-')
      .join('-');
    const created_time: string = date.split('T')[1].split('.')[0];
    this.completionDate = created_date + ' ' + created_time;
  }
  isDataString(data: string | Moment): data is string {
    return typeof data === 'string';
  }
}
