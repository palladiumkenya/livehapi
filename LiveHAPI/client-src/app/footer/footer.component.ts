import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'liveapp-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {
  version = ' v1.0.4';
  title = 'LiveHAPI';
  constructor() { }

  ngOnInit() {

  }

}
