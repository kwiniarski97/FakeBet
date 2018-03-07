import {Component, OnInit} from '@angular/core';
import {Match} from '../../models/match';
import {MatchService} from '../../services/match.service';
import 'rxjs/add/operator/switchMap';
import {ActivatedRoute, Params} from '@angular/router';
import {DateTimeHelper} from '../../helpers/datetimehelper';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.css']
})
export class MatchComponent implements OnInit {

  loading = true;

  match: Match;

  time: string;

  constructor(private route: ActivatedRoute, private service: MatchService) {
  }


  ngOnInit() {
    this.route.params
      .switchMap((params: Params) => this.service.get(params['id']))
      .subscribe((response) => {
        this.match = JSON.parse(response._body);
        this.time = DateTimeHelper.ParseDate(this.match.matchTime);
        this.loading = false;
      });
  }
}
