import {Component, OnInit} from '@angular/core';
import {MatchService} from '../../services/match.service';
import {Match} from '../../models/match';
import {DateTimeHelper} from '../../helpers/datetimehelper';
import {LocalStorageService} from '../../services/localstorage.service';
import {BetService} from '../../services/bet.service';
import {Bet} from '../../models/bet';
import {AlertService} from '../../services/alert.service';

@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.css']
})
export class MatchesComponent implements OnInit {

  matches: Match[];

  loading = true;

  time: string;

  constructor(private matchService: MatchService, private dateHelper: DateTimeHelper,
              private localStorage: LocalStorageService, private betService: BetService, private alertService: AlertService) {

  }

  ngOnInit() {
    this.matchService.getNotStarted().subscribe(response => {
      this.matches = response;
      this.loading = false;
    });
  }

  private getParsedDate(date: Date): string {
    return this.dateHelper.ParseDate(date);
  }

  private placeBet(matchId: string, pointsOnA: any, pointsOnB: any): void {
    // todo check if values are valid and check if user is logged if not then make some alert
    const user = JSON.parse(localStorage.getItem('currentUser'));
    if (!user) {
      this.alertService.emitError('You must be logged'); // todo zrob tutaj modala z errorem
    }
    if ((pointsOnA.value + pointsOnB.value) == 0) {
      return;
    }
    const bet = new Bet(matchId, user.nickName, pointsOnA.value, pointsOnB.value);

    this.betService.addBet(bet).subscribe(data => {
    }, error => {
      this.alertService.emitError(error._body);
    });
  }
}
