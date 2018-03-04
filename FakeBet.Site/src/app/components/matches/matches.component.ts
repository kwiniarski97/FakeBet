import {Component, OnInit} from '@angular/core';
import {MatchService} from '../../services/match.service';
import {Match} from '../../models/match';

@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.css']
})
export class MatchesComponent implements OnInit {

  matches: Match[];

  loading = true;

  constructor(private service: MatchService) {

  }

  ngOnInit() {
    this.service.getNotStarted().subscribe(data => {
      this.matches = data;
      this.loading = false;
    });
  }

}
