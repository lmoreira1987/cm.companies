import { LoginComponent } from "./pages/login/login.component";
import { ListComponent } from "./pages/list/list.component";
import { DashboardComponent } from "./pages/dashboard/dashboard.component";

import { LayoutsComponent } from "./pages/layouts/layouts.component";

import { AbsoluteLayoutComponent } from "./pages/layouts/absolute-layout/absolute-layout.component";
import { DockLayoutComponent } from "./pages/layouts/dock-layout/dock-layout.component";
import { FlexboxLayoutComponent } from "./pages/layouts/flexbox-layout/flexbox-layout.component";
import { GridLayoutComponent } from "./pages/layouts/grid-layout/grid-layout.component";
import { StackLayoutComponent } from "./pages/layouts/stack-layout/stack-layout.component";
import { WrapLayoutComponent } from "./pages/layouts/wrap-layout/wrap-layout.component";
import { MixedLayoutComponent } from "./pages/layouts/mixed-layout/mixed-layout.component";

// User
import { CreateUserComponent } from "./pages/forms/create/create-user.component";
import { ListUserComponent } from "./pages/forms/list/list-user.component";
import { UpdateUserComponent } from "./pages/forms/update/update-user.component";

import { HeaderActionBarComponent } from "./pages/shared/header-action-bar/header-action-bar.component";  

export const routes = [
  { path: "", component: LoginComponent }, 
  { path: "list", component: ListComponent },
  { path: "dashboard", component: DashboardComponent },
  { path: "layouts", component: LayoutsComponent },
  { path: "absolutelayout", component: AbsoluteLayoutComponent },
  { path: "docklayout", component: DockLayoutComponent },
  { path: "flexboxlayout", component: FlexboxLayoutComponent },
  { path: "gridlayout", component: GridLayoutComponent },
  { path: "stacklayout", component: StackLayoutComponent },
  { path: "wraplayout", component: WrapLayoutComponent },
  { path: "mixedlayout", component: MixedLayoutComponent },
  { path: "createuser", component: CreateUserComponent },
  { path: "listuser", component: ListUserComponent },
  { path: "updateuser", component: UpdateUserComponent }
];

export const navigatableComponents = [
  LoginComponent,
  ListComponent,
  DashboardComponent,
  LayoutsComponent,
  AbsoluteLayoutComponent,
  DockLayoutComponent,
  FlexboxLayoutComponent,
  GridLayoutComponent,
  StackLayoutComponent,
  WrapLayoutComponent,
  MixedLayoutComponent,
  CreateUserComponent,
  ListUserComponent,
  UpdateUserComponent,
  HeaderActionBarComponent
];