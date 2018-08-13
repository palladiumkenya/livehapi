import { Component, OnInit } from '@angular/core';
import {Stats} from '../model/stats';

@Component({
  selector: 'liveapp-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit {

    public stats: Stats;
  constructor() {
      this.stats = {
          received: 10,
          sent: 9,
          failed: 1
      };
  }

  ngOnInit() {
  }

}
