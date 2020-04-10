import { Component, ElementRef, NgZone, OnInit, ViewChild } from "@angular/core";
import { TextField } from "ui/text-field";
import * as SocialShare from "nativescript-social-share";
import { Page } from "tns-core-modules/ui/page/page";
import { Router } from "@angular/router";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { UserService } from "../../../shared/user/user.service";
import { User } from "../../../shared/user/user";

@Component({
  selector: "mixed-layout",
  moduleId: module.id,
  providers: [UserService],
  templateUrl: "./mixed-layout.html",
  styleUrls: ["./mixed-layout-common.css", "./mixed-layout.css"]
})
export class MixedLayoutComponent implements OnInit {

  constructor(private userService: UserService) {}

  users: Array<User> = [];

  ngOnInit() {
    console.log('entrou');
    this.userService.getUsers()
      .subscribe((list: any) => this.users = list), 
        (error) => alert("Unfortunately we could not find users.");
  }

  share() {
    let listString = "Dashboard Component Page"
    SocialShare.shareText(listString);
  }
  
}