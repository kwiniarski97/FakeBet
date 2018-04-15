import {Component, OnInit} from '@angular/core';
import {MatchService} from '../../services/match.service';
import {Match} from '../../models/match';
import {LocalStorageService} from '../../services/localstorage.service';
import {BetService} from '../../services/bet.service';
import {Bet} from '../../models/bet';
import {AlertService} from '../../services/alert.service';
import {User} from '../../models/user';

@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.css']
})
export class MatchesComponent implements OnInit {

  matches: Match[];

  loading = true;

  time: string;

  constructor(private matchService: MatchService, private localStorageService: LocalStorageService, private betService: BetService,
              private alertService: AlertService) {

  }

  ngOnInit() {
    this.matchService.getNotStarted().subscribe(response => {
      this.matches = response;
      for (let i = 0; i < this.matches.length; i++) {
        this.matches[i].matchTime = new Date(this.matches[i].matchTime.toString() + '+0000');
      }
      this.matches = this.matches.sort(function (a, b) {
        return ((new Date(b.matchTime).getTime()) - (new Date(a.matchTime)).getTime()) * -1;
      });
      this.loading = false;
    });
  }


  private placeBet(matchId: string, pointsOnA: any, pointsOnB: any): void {
    const user = this.localStorageService.retrieve('currentUser') as User;
    if (!user) {
      this.alertService.emitError('You must be logged');
      return;
    }
    if ((pointsOnA.value + pointsOnB.value) == 0) {
      return;
    }

    const bet = new Bet(matchId, user.nickName, pointsOnA.value, pointsOnB.value);

    this.betService.addBet(bet).subscribe(data => {
      this.alertService.emitOk('Bet placed');
      const storedUser = this.localStorageService.retrieve('currentUser') as User;
      storedUser.points -= ((bet.betOnTeamA as number) + (bet.betOnTeamB as number));
      this.localStorageService.save('currentUser', storedUser);
    }, error => {
      this.alertService.emitError(error._body);
    });
  }
}
