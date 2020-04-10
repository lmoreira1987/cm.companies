import { Injectable } from "@angular/core";
import { Http, Headers, Response, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/do";
import "rxjs/add/operator/map";

import { User } from "./user";
import { Config } from "../config";

@Injectable()
export class UserService {
  constructor(private http: Http) {}

  register(user: User) {
    return this.http.post(`${Config.apiUrl}user/`, user, { headers: this.getCommonHeaders()})
      .catch(this.handleErrors);
  }

  login(user: User) {
    return this.http.get(`${Config.apiUrl}User/Login?email=${user.email}&password=${user.password}`, { headers: this.getCommonHeaders() })
      .map(response => response.json())
      .catch(this.handleErrors);
  }

  getUsers() {
    return this.http.get(`${Config.apiUrl}User`, { headers: this.getCommonHeaders() })
      .map(response => response.json())
      .catch(this.handleErrors);
  }

  getCommonHeaders() {
    let headers = new Headers();
    headers.append("Content-Type", "application/json");
    return headers;
  }

  handleErrors(error: Response) {
    console.log(JSON.stringify(error.json()));
    return Observable.throw(error);
  }
}