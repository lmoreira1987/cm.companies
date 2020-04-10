import { Component, ElementRef, NgZone, OnInit, ViewChild } from "@angular/core";
import { TextField } from "ui/text-field";
import * as SocialShare from "nativescript-social-share";
import { Page } from "tns-core-modules/ui/page/page";
import { Router } from "@angular/router";

@Component({
  selector: "header-action-bar",
  moduleId: module.id,
  templateUrl: "./header-action-bar.html",
  styleUrls: ["./header-action-bar-common.css", "./header-action-bar.css"]
})
export class HeaderActionBarComponent implements OnInit {

  @ViewChild("pageCreateUser") pageCreateUser: ElementRef;

  constructor(private zone: NgZone,
    private router: Router, 
    private page: Page) {}

  ngOnInit() {

  }

  goToPage(page:string) {
    this.router.navigate([`/${page}`])
  }

  share() {
    let listString = "Dashboard Component Page"
    SocialShare.shareText(listString);
  }
  
}