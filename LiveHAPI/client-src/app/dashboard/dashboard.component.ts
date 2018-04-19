import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'liveapp-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  info = 'This is the Afya Mobile API that will allow EMR subscribers access to data sent from devices running Afya Mobile.';

  constructor() { }

  ngOnInit() {
  }

}
