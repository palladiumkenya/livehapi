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
import {InputTextModule, MessageModule} from 'primeng/primeng';


@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    TopBarComponent,
    DashboardComponent,
    ConfigComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    AppRoutes,
      ButtonModule,
      InputTextModule,
     MessageModule,
   ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
