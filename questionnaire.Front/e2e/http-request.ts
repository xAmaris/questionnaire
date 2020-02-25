import { protractor } from 'protractor';

// tslint:disable:no-implicit-dependencies
// tslint:disable:no-var-requires
const request = require('request');

const jar = request.jar();
const req = request.defaults({
  jar
});
export const httpPostRequest = (url: string, params: any) => {
  const defer = protractor.promise.defer();
  req.post(
    {
      url: apiUrl + url,
      body: params,
      json: true
    },
    (error: any, message: any) => {
      if (error || message.statusCode >= 400) {
        defer.reject({
          error,
          message
        });
      } else {
        defer.fulfill(message);
      }
    }
  );
  return defer.promise;
};

export const apiUrl = 'http://localhost:5000/api/';
