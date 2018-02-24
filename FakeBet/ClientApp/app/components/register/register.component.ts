import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {AlertService} from "../../services/alert.service";
import {UserService} from "../../services/user.service";

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

    model: any = {};
    processing = false;

    constructor(private router: Router, private userService: UserService, private alertService: AlertService) {
    }

    ngOnInit() {
    }

    register() {
        this.processing = true;
        this.userService.register(this.model).subscribe(data => {
                this.alertService.success('Registation successful', true);
                this.router.navigate(['/login']);
            },
            error => {
                this.alertService.error(error._body);
                this.processing = false;
            });
    }

}
