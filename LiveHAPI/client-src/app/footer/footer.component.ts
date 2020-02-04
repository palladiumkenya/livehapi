import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';
import {ConfigService} from '../services/config.service';
import {environment} from '../../environments/environment';

@Component({
    selector: 'liveapp-footer',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit,OnDestroy {

    version = ' v1.0.4';
    title = 'LiveHAPI';
    stageName = '';
    $version: Subscription;

    constructor(private configService: ConfigService) {
        this.stageName = environment.stageName;
    }

    ngOnInit() {
        this.$version = this.configService.getVersion()
            .subscribe(
                p => {
                    this.version = `v${p}`;
                },
                e => {
                },
                () => {
                }
            );
    }

    ngOnDestroy(): void {
       if(this.$version)
       {
           this.$version.unsubscribe();
       }
    }

}
