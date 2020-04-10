import { Component, ElementRef, NgZone, OnInit, ViewChild } from "@angular/core";
import { TextField } from "ui/text-field";
import * as SocialShare from "nativescript-social-share";
import { Page } from "tns-core-modules/ui/page/page";
import { Router } from "@angular/router";

import * as absoluteLayoutModule from "tns-core-modules/ui/layouts/absolute-layout";
import * as colorModule from "tns-core-modules/color";
import * as labelModule from "tns-core-modules/ui/label";

@Component({
  selector: "absolute-layout",
  moduleId: module.id,
  templateUrl: "./absolute-layout.html",
  styleUrls: ["./absolute-layout-common.css", "./absolute-layout.css"]
})
export class AbsoluteLayoutComponent implements OnInit {

  constructor(private zone: NgZone,
    private router: Router, 
    private page: Page) {}

  ngOnInit() {
    
  }

  share() {
    let listString = "Dashboard Component Page"
    SocialShare.shareText(listString);
  }
  
}