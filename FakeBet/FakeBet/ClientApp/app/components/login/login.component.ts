import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {AlertService} from "../../services/alert.service";
import {AuthenticationService} from "../../services/authentication.service";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    providers: [AuthenticationService,AlertService]
})
export class LoginComponent implements OnInit {

    model: any = {};
    processing = false;
    returnUrl: string;

    constructor(private route: ActivatedRoute,
                private router: Router,
                private authentivationService: AuthenticationService,
                private alertService: AlertService) {
    }

    ngOnInit() {
        this.authentivationService.logout();
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }
    
    login(){
        this.processing = true;
        this.authentivationService.login(this.model.nickname, this.model.password).subscribe(data=>{
            this.router.navigate([this.returnUrl]);
        },
            error => {
            this.alertService.error('Username or password is incorrect');
            this.processing = false;
            });
    }

}
