import {Component, OnDestroy, OnInit} from '@angular/core';
import {Stats} from '../model/stats';
import {Subscription} from '../../../node_modules/rxjs/Subscription';
import {StatsService} from '../services/stats.service';
import {Message} from '../../../node_modules/primeng/api';

@Component({
  selector: 'liveapp-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit, OnDestroy {

    public messages: Message[];

    public stats$: Subscription;
    public stats: Stats;

    public constructor(private statsService: StatsService) {
    }

    public ngOnInit() {

        this.stats$ = this.statsService.getStats()
            .subscribe(
                p => {
                    this.stats = p;
                },
                e => {
                    this.messages.push({severity: 'error', summary: 'Error Loading', detail: <any>e});
                },
                () => {

                }
            );
    }

    public ngOnDestroy(): void {
        if (this.stats$) {
            this.stats$.unsubscribe();
        }
    }
}
