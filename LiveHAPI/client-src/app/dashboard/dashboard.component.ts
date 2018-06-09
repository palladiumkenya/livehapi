import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'liveapp-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  info = 'This is the Afya Mobile API that will allow data synchronization with your EMR';

  public constructor() { }

  public ngOnInit() {
  }

}
