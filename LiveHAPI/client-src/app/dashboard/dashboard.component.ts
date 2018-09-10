import { Component, OnInit } from '@angular/core';
import {BreadcrumbService} from '../breadcrumb.service';

@Component({
  selector: 'liveapp-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  info = 'This is the Afya Mobile API that will allow data synchronization with your EMR';

  public constructor(public breadcrumbService: BreadcrumbService) {
      this.breadcrumbService.setItems([
          {label: 'Dashboard'}
      ]);
  }

  public ngOnInit() {
  }

}
