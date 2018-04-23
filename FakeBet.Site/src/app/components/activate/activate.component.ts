import {Component, OnInit} from '@angular/core';
import {UserService} from '../../services/user.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-activate',
  templateUrl: './activate.component.html',
  styleUrls: ['./activate.component.css']
})
export class ActivateComponent implements OnInit {

  private encodedNickName: string;
  private sub: any;

  constructor(private userService: UserService, private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.encodedNickName = params['encodedNickName']; // (+) converts string 'id' to a number

      this.userService.activate(this.encodedNickName).subscribe();

      this.router.navigate(['/']);
    });


  }

}
