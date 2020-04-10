import { Component, ElementRef, NgZone, OnInit, ViewChild } from "@angular/core";
import { TextField } from "ui/text-field";
import * as SocialShare from "nativescript-social-share";
import { Page } from "tns-core-modules/ui/page/page";
import { Router } from "@angular/router";

@Component({
  selector: "flexbox-layout",
  moduleId: module.id,
  templateUrl: "./flexbox-layout.html",
  styleUrls: ["./flexbox-layout-common.css", "./flexbox-layout.css"]
})
export class FlexboxLayoutComponent implements OnInit {

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