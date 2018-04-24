import {Component, OnInit} from '@angular/core';
import {Match, MatchStatus} from '../../../models/match';
import {MatchService} from '../../../services/match.service';
import {AlertService} from '../../../services/alert.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {CountryCodesService} from '../../../services/country-codes.service';

@Component({
  selector: 'app-admin-match',
  templateUrl: './admin-match.component.html',
  styleUrls: ['./admin-match.component.css']
})
export class AdminMatchComponent implements OnInit {

  loading = false;

  matches: Match[];

  selectedMatch: Match = new Match();


  constructor(private matchService: MatchService, private alertService: AlertService, private modalService: NgbModal,
              public countryCodesService: CountryCodesService) {
  }

  ngOnInit() {
  }

  public getMatchStatus(matchStatus: MatchStatus) {
    return MatchStatus[matchStatus];
  }

  public openUpdateMatchModal(modal: any, match: Match) {
    Object.assign(this.selectedMatch, match);
    this.selectedMatch.matchTime = new Date(this.selectedMatch.matchTime);
    this.modalService.open(modal, {size: 'lg'});
  }

  public openEndMatchModal(endMatchModal: any, match: Match) {
    Object.assign(this.selectedMatch, match);
    this.modalService.open(endMatchModal);
  }

  public openAddMatchModal(modal: any) {
    this.selectedMatch = new Match();
    this.modalService.open(modal, {size: 'lg'});
  }

  public deleteMatch(matchId: string) {
    if (this.alertService.emitConfirm(`Are you sure you want to delete match with id: ${matchId}?`)) {
      this.matchService.delete(matchId).subscribe(response => {
        this.alertService.emitOk('Match deleted');
      }, error => {
        this.alertService.emitError(error['_body']);
      });
    }
  }

  public getAllMatches() {
    this.matchService.getAll().subscribe(response => {
      const matches = JSON.parse(response['_body']);
      this.matches = matches;
      for (let i = 0; i < this.matches.length; i++) {
        this.matches[i].matchTime = new Date(this.matches[i].matchTime.toString() + '+0000');
      }
    }, error => {
      this.alertService.emitError(error['_body']);
    });
  }


  public addMatch() {
    this.matchService.add(this.selectedMatch).subscribe(response => {
      this.alertService.emitOk('Match has been created');
      this.getAllMatches();
    }, error => {
      this.alertService.emitError(error['_body']);
    });
  }

  public updateMatch() {
    this.matchService.update(this.selectedMatch).subscribe(response => {
      this.alertService.emitOk('Match has been updated');
      this.getAllMatches();
    }, error => {
      this.alertService.emitError(error['_body']);
    });
  }

  public endMatch(winner: string) {
    this.matchService.end(this.selectedMatch.matchId, winner).subscribe(response => {
      this.alertService.emitOk('Match has been ended');
      this.getAllMatches();
    }, error => {
      this.alertService.emitError(error['_body']);
    });
  }
}
