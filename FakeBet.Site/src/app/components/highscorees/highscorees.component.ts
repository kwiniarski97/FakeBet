import {Component, OnInit} from '@angular/core';
import {UserTop} from '../../models/usertop';
import {UserService} from '../../services/user.service';

@Component({
  selector: 'app-highscorees',
  templateUrl: './highscorees.component.html',
  styleUrls: ['./highscorees.component.css']
})
export class HighscoreesComponent implements OnInit {

  topUsers: UserTop[];
  top1 = new UserTop();
  top2 = new UserTop();
  top3 = new UserTop();

  loading = true;

  constructor(private userService: UserService) {
  }

  ngOnInit() {
    this.userService.getTopUsers().subscribe(response => {
      const temp = JSON.parse(response['_body']);
      this.top1 = temp[0];
      this.top2 = temp[1];
      this.top3 = temp[2];
      temp.splice(0, 3);
      this.topUsers = temp;
      this.loading = false;
    });
  }

}
