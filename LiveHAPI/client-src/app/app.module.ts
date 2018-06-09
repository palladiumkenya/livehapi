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
import {ConfirmationService, InputTextModule, MessageModule, MessagesModule} from 'primeng/primeng';
import {HttpClientModule} from '@angular/common/http';
import {ConfigService} from './services/config.service';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { SyncConfigComponent } from './sync-config/sync-config.component';


@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    TopBarComponent,
    DashboardComponent,
    ConfigComponent,
    SyncConfigComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    AppRoutes,
      ButtonModule,
      InputTextModule,
     MessageModule,
      MessagesModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule
   ],
  providers: [
      ConfigService,  ConfirmationService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
