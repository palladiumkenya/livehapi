import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { FooterComponent } from './footer/footer.component';
import { TopBarComponent } from './top-bar/top-bar.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import {AppRoutes} from './app.routes';
import {RouterModule} from '@angular/router';
import { ConfigComponent } from './config/config.component';
import {ButtonModule} from 'primeng/button';
import {
    ConfirmationService,
    DataTableModule,
    InputTextModule,
    MessageModule,
    MessagesModule,
    ToolbarModule,
    TreeModule
} from 'primeng/primeng';
import {HttpClientModule} from '@angular/common/http';
import {ConfigService} from './services/config.service';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { SyncConfigComponent } from './sync-config/sync-config.component';
import {AppMenuComponent} from './app-menu/app-menu.component';
import {AppBreadcrumbComponent} from './app-breadcrumb/app-breadcrumb.component';
import {BreadcrumbService} from './breadcrumb.service';
import {AppSubmenuComponent} from './app-submenu/app-submenu.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { StatsComponent } from './stats/stats.component';
import { ClientManagerComponent } from './client-manager/client-manager.component';
import {StatsService} from './services/stats.service';
import {ClientManagerService} from './services/client-manager.service';
import { ClientTreeComponent } from './contacts/client-tree/client-tree.component';
import { PrimaryListComponent } from './contacts/primary-list/primary-list.component';
import {ClientContactsService} from './contacts/services/client-contacts.service';
import { ContactTreeComponent } from './contacts/contact-tree/contact-tree.component';
import { PrimarytreeListComponent } from './contacts/primarytree-list/primarytree-list.component';


@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    TopBarComponent,
    DashboardComponent,
    ConfigComponent,
    SyncConfigComponent,
      AppMenuComponent,
      AppBreadcrumbComponent,
      AppSubmenuComponent,
      StatsComponent,
      ClientManagerComponent,
      ClientTreeComponent,
      PrimaryListComponent,
      ContactTreeComponent,
      PrimarytreeListComponent
  ],
  imports: [
    BrowserModule,
      BrowserAnimationsModule,
      DataTableModule,
      ToolbarModule,
    RouterModule,
    AppRoutes,
      ButtonModule,
      InputTextModule,
     MessageModule,
      MessagesModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      TreeModule
   ],
  providers: [
      ConfigService,  ConfirmationService, BreadcrumbService, StatsService,
      ClientManagerService, ClientContactsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
