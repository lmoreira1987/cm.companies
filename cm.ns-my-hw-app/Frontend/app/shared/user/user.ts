var validator = require("email-validator");

export class User {
  email: string;
  name: string;
  password: string;
  
  isValidEmail() {
    return validator.validate(this.email);
  }
}