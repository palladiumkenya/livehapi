import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'liveapp-top-bar',
  templateUrl: './top-bar.component.html',
  styleUrls: ['./top-bar.component.scss']
})
export class TopBarComponent implements OnInit {
  title = 'LiveHAPI';
  constructor() { }

  ngOnInit() {
  }

}
