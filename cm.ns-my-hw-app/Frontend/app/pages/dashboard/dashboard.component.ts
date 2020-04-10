import { Component, ElementRef, NgZone, OnInit, ViewChild } from "@angular/core";
import { DashboardService } from "../../shared/dashboard/dashboard.service";
import { Page } from "ui/page";
import { TextField } from "ui/text-field";
import * as SocialShare from "nativescript-social-share";
import { Router } from "@angular/router";
import { Color } from "tns-core-modules/color/color";
import { RouterExtensions } from "nativescript-angular/router";

@Component({
  selector: "dashboard",
  moduleId: module.id,
  templateUrl: "./dashboard.html",
  styleUrls: ["./dashboard-common.css", "./dashboard.css"],
  providers: [DashboardService]
})
export class DashboardComponent implements OnInit {

  @ViewChild('pageDashboard') pageDashboard: ElementRef;

  constructor(private router: Router, 
    private dashboardService: DashboardService, 
    private page: Page,
    private routerExtensions: RouterExtensions,
    private zone: NgZone) {}

  ngOnInit() {
    let pageDashboard = <Page>this.pageDashboard.nativeElement;
    pageDashboard.backgroundImage = "res://bg_technology";
  }

  onNavBtnTap(){
    this.routerExtensions.back();
  }

  goToLayouts() {
    this.router.navigate(["/layouts"])
  }

  goToUsers() {
    this.router.navigate(["/listuser"])
  }

  share() {
    let listString = "Dashboard Component Page"
    SocialShare.shareText(listString);
  }
  
}