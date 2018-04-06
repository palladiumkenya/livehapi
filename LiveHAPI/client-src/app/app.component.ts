import { Component } from '@angular/core';

@Component({
  selector: 'liveapp-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  version = 'v1.0.0';
  released = '05 APR 2018';
  title = 'LiveHAPI';
  info = 'This is the Afya Mobile API that will allow EMR subscribers access to data sent from devices running Afya Mobile.';
}
