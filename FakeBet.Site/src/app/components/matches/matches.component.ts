import {Component, OnInit} from '@angular/core';
import {MatchService} from '../../services/match.service';
import {Match} from '../../models/match';
import {LocalStorageService} from '../../services/localstorage.service';
import {BetService} from '../../services/bet.service';
import {Bet} from '../../models/bet';
import {AlertService} from '../../services/alert.service';
import {User} from '../../models/user';
import {TtlEntity} from '../../models/ttlEntity';

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
    this.getMatches();
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
      storedUser.points -= bet.betOnTeamA as number;
      storedUser.points -= bet.betOnTeamB as number;
      this.localStorageService.save('currentUser', storedUser);
      this.getMatchesFromApiAndCacheIt();
    }, error => {
      this.alertService.emitError(error._body);
      this.getMatchesFromApiAndCacheIt();
    });

  }

  private getMatches() {
    if (this.isCachedMatchesReliable()) {
      this.getMatchesFromCache();
    } else {
      this.getMatchesFromApiAndCacheIt();
    }

  }

  private getMatchesFromCache() {
    const entity = this.localStorageService.retrieve('matches') as TtlEntity;
    this.matches = entity.content;
    this.loading = false;
  }

  private getMatchesFromApiAndCacheIt() {
    this.matchService.getNotStarted().subscribe(response => {
      this.matches = response;
      this.sortMatchesByStartTime();
      this.cacheMatches();
      this.loading = false;
    });
  }

  private sortMatchesByStartTime() {
    for (let i = 0; i < this.matches.length; i++) {
      this.matches[i].matchTime = new Date(this.matches[i].matchTime.toString() + '+0000');
    }
    this.matches = this.matches.sort(function (a, b) {
      return ((new Date(b.matchTime).getTime()) - (new Date(a.matchTime)).getTime()) * -1;
    });
  }

  private isCachedMatchesReliable() {
    const matchesCached = this.localStorageService.retrieve('matches') as TtlEntity;
    // if diff between now and time when timestamp is cached is lower than 5 minutes it will return true
    return (matchesCached && Math.round(Date.now() - matchesCached.timeStamp) < (5 * 1000 * 60)); // 5 * 1000 * 60 5 minutes in millisec

  }

  private cacheMatches() {
    const entity = new TtlEntity(Date.now(), this.matches);
    this.localStorageService.save('matches', entity);
  }


}
