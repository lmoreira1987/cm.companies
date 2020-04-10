import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { User } from "../../shared/user/user";
import { UserService } from "../../shared/user/user.service";
import { Router } from "@angular/router";
import { Page } from "ui/page";
import { Color } from "color";
import { View } from "ui/core/view";
import { setHintColor } from "../../utils/hint-util";
import { TextField } from "ui/text-field";
import { BaseResponse } from "../../shared//base/base-response.component";

@Component({
  selector: "my-app",
  providers: [UserService],
  templateUrl: "./pages/login/login.html",
  styleUrls: ["pages/login/login-common.css", "pages/login/login.css"]
})
export class LoginComponent implements OnInit {
  user: User;
  isLoggingIn = true;
  @ViewChild("container") container: ElementRef;
  @ViewChild("email") email: ElementRef;
  @ViewChild("password") password: ElementRef;

  constructor(private router: Router, 
              private userService: UserService, 
              private page: Page) {
    this.user = new User();
    this.user.email = "abc@abc.com";
    this.user.password = "123";
  }

  ngOnInit() {
    this.page.actionBarHidden = true;
    this.page.backgroundImage = "res://bg_technology";
  }

  public get message(): string {
    return "test message";
  }

  submit() {
    if (!this.user.isValidEmail()) {
      alert("Enter a valid email address.");
      return;
    }

    if (this.isLoggingIn) {
      this.login(this.user);
    } else {
      this.signUp();
    }
  }

  login(user: User) {
    this.userService.login(user)
      .subscribe(() => this.router.navigate(["/dashboard"]), 
        (error) => alert("Unfortunately we could not find your account."));
  }

  signUp() {
    this.userService.register(this.user)
      .subscribe(
        () => {
          alert("Your account was successfully created.");
          this.toggleDisplay();
        },
        () => alert("Unfortunately we were unable to create your account.")
      );
  }

  toggleDisplay() {
    this.isLoggingIn = !this.isLoggingIn;
    this.setTextFieldColors();
    let container = <View>this.container.nativeElement;
    container.animate({
      backgroundColor: this.isLoggingIn ? new Color("white") : new Color("#d7e4f0"),
      duration: 200
    });
  }

  setTextFieldColors() {
    let emailTextField = <TextField>this.email.nativeElement;
    let passwordTextField = <TextField>this.password.nativeElement;
  
    let mainTextColor = new Color(this.isLoggingIn ? "black" : "black");
    emailTextField.color = mainTextColor;
    passwordTextField.color = mainTextColor;
  
    let hintColor = new Color(this.isLoggingIn ? "#ACA6A7" : "#ACA6A7");
    setHintColor({ view: emailTextField, color: hintColor });
    setHintColor({ view: passwordTextField, color: hintColor });
  }
}