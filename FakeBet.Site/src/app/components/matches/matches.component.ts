import {Component, OnInit} from '@angular/core';
import {MatchService} from '../../services/match.service';
import {Match} from '../../models/match';
import {Router} from '@angular/router';
import {DateTimeHelper} from '../../helpers/datetimehelper';

@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.css']
})
export class MatchesComponent implements OnInit {

  matches: Match[];

  loading = true;

  time: string;

  constructor(private service: MatchService, private router: Router, private dateHelper: DateTimeHelper) {

  }

  ngOnInit() {
    this.service.getNotStarted().subscribe(response => {
      this.matches = response;
      this.loading = false;
    });
  }

  private getParsedDate(date: Date): string {
    return this.dateHelper.ParseDate(date);
  }

  private placeBet(matchId: string, pointsOnA: any, pointsOnB: any): void {
    // todo check if values are valid and check if user is logged if not then make some alert
    console.log(matchId + ' ' + pointsOnA.value + ' ' + pointsOnB.value);
  }
}
