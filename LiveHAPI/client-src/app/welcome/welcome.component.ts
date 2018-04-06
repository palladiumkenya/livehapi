import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'liveapp-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {
  info = 'This is the Afya Mobile API that will allow EMR subscribers access to data sent from devices running Afya Mobile.';
  constructor() { }

  ngOnInit() {
  }

}
