import { Component, ElementRef, NgZone, OnInit, ViewChild } from "@angular/core";
import { TextField } from "ui/text-field";
import * as SocialShare from "nativescript-social-share";
import { Page } from "tns-core-modules/ui/page/page";
import { Router } from "@angular/router";
import { Color } from "tns-core-modules/color/color";

@Component({
  selector: "layouts",
  moduleId: module.id,
  templateUrl: "./layouts.html",
  styleUrls: ["./layouts-common.css", "./layouts.css"]
})
export class LayoutsComponent implements OnInit {

  @ViewChild("pageLayouts") pageLayouts: ElementRef;

  constructor(private zone: NgZone,
    private router: Router, 
    private page: Page) {}

  ngOnInit() {
    let pageLayouts = <Page>this.pageLayouts.nativeElement;
    pageLayouts.backgroundImage = "res://bg_technology";
  }

  goToPage(page:string) {
    this.router.navigate([`/${page}`])
  }

  share() {
    let listString = "Layouts Component Page"
    SocialShare.shareText(listString);
  }
  
}