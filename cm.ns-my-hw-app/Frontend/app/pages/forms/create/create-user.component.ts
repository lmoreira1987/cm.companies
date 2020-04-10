import { Component, ElementRef, NgZone, OnInit, ViewChild } from "@angular/core";
import { TextField } from "ui/text-field";
import * as SocialShare from "nativescript-social-share";
import { Page } from "tns-core-modules/ui/page/page";
import { Router } from "@angular/router";

@Component({
  selector: "create-user",
  moduleId: module.id,
  templateUrl: "./create-user.html",
  styleUrls: ["./create-user-common.css", "./create-user.css"]
})
export class CreateUserComponent implements OnInit {

  @ViewChild("pageCreateUser") pageCreateUser: ElementRef;

  constructor(private zone: NgZone,
    private router: Router, 
    private page: Page) {}

  ngOnInit() {
    let pageCreateUser = <Page>this.pageCreateUser.nativeElement;
    pageCreateUser.backgroundImage = "res://bg_technology";
  }

  goToPage(page:string) {
    this.router.navigate([`/${page}`])
  }

  share() {
    let listString = "Dashboard Component Page"
    SocialShare.shareText(listString);
  }
  
}