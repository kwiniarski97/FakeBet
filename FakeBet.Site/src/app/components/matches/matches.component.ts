import {Component, OnInit} from '@angular/core';
import {MatchService} from '../../services/match.service';
import {Match} from '../../models/match';
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

  constructor(private matchService: MatchService, private localStorage: LocalStorageService, private betService: BetService,
              private alertService: AlertService) {

  }

  ngOnInit() {
    this.matchService.getNotStarted().subscribe(response => {
      this.matches = response;
      this.matches = this.matches.sort(function (a, b) {
        return ((new Date(b.matchTime).getTime()) - (new Date(a.matchTime)).getTime()) * -1;
      });
      this.loading = false;
    });
  }


  private placeBet(matchId: string, pointsOnA: any, pointsOnB: any): void {
    const user = JSON.parse(localStorage.getItem('currentUser'));
    if (!user) {
      this.alertService.emitError('You must be logged');
    }
    if ((pointsOnA.value + pointsOnB.value) == 0) {
      return;
    }

    const bet = new Bet(matchId, user.nickName, pointsOnA.value, pointsOnB.value);

    this.betService.addBet(bet).subscribe(data => {
      this.alertService.emitOk('Bet placed');
    }, error => {
      this.alertService.emitError(error._body);
    });
  }
}
