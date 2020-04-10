import { Component, ElementRef, NgZone, OnInit, ViewChild } from "@angular/core";
import { TextField } from "ui/text-field";
import * as SocialShare from "nativescript-social-share";
import { Page } from "tns-core-modules/ui/page/page";
import { Router } from "@angular/router";

@Component({
  selector: "list-user",
  moduleId: module.id,
  templateUrl: "./list-user.html",
  styleUrls: ["./list-user-common.css", "./list-user.css"]
})
export class ListUserComponent implements OnInit {

  @ViewChild("pageUser") pageUser: ElementRef;

  constructor(private zone: NgZone,
    private router: Router, 
    private page: Page) {}

  ngOnInit() {
    let pageUser = <Page>this.pageUser.nativeElement;
    pageUser.backgroundImage = "res://bg_technology";
  }

  goToPage(page:string) {
    this.router.navigate([`/${page}`])
  }

  share() {
    let listString = "Dashboard Component Page"
    SocialShare.shareText(listString);
  }
  
}