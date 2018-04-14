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

  private getMatchStatus(matchStatus: MatchStatus) {
    return MatchStatus[matchStatus];
  }

  private openUpdateMatchModal(modal: any, match: Match) {
    Object.assign(this.selectedMatch, match);
    const dupa = new Date(this.selectedMatch.matchTime);
    this.selectedMatch.matchTime = dupa;
    this.modalService.open(modal, {size: 'lg'});
  }

  private openEndMatchModal(endMatchModal: any, match: Match) {
    Object.assign(this.selectedMatch, match);
    this.modalService.open(endMatchModal);
  }

  private openAddMatchModal(modal: any) {
    this.selectedMatch = new Match();
    this.modalService.open(modal, {size: 'lg'});
  }

  private deleteMatch(matchId: string) {
    if (this.alertService.emitConfirm(`Are you sure you want to delete match with id: ${matchId}?`)) {
      this.matchService.delete(matchId).subscribe(response => {
        this.alertService.emitOk('Match deleted');
      }, error => {
        this.alertService.emitError(error['_body']);
      });
    }
  }

  private getAllMatches() {
    this.matchService.getAll().subscribe(response => {
      const matches = JSON.parse(response['_body']);
      this.matches = matches;
    }, error => {
      this.alertService.emitError(error['_body']);
    });
  }


  private addMatch() {
    this.matchService.add(this.selectedMatch).subscribe(response => {
      this.alertService.emitOk('Match has been created');
      this.getAllMatches();
    }, error => {
      this.alertService.emitError(error['_body']);
    });
  }

  private updateMatch() {
    this.matchService.update(this.selectedMatch).subscribe(response => {
      this.alertService.emitOk('Match has been updated');
      this.getAllMatches();
    }, error => {
      this.alertService.emitError(error['_body']);
    });
  }

  private endMatch(winner: string) {
    this.matchService.end(this.selectedMatch.matchId, winner).subscribe(response => {
      this.alertService.emitOk('Match has been ended');
      this.getAllMatches();
    }, error => {
      this.alertService.emitError(error['_body']);
    });
  }
}
