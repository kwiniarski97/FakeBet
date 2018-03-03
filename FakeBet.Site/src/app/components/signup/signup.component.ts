import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {AlertService} from '../../services/alert.service';
import {UserService} from '../../services/user.service';
import {Response} from '@angular/http';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  model: any = {};
  processing = false;

  constructor(private router: Router, private userService: UserService, private alertService: AlertService) {
  }

  ngOnInit() {
  }

  register() {
    this.processing = true;
    this.userService.register(this.model).subscribe(data => {
      this.alertService.success('Registration successful', true);
      this.router.navigate(['/home']);
    }, error => {
      this.processing = false;
      this.alertService.error(error.body);
    });
  }
}
