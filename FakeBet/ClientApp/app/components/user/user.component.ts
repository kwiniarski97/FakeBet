import {Component, Inject, OnInit} from '@angular/core';
import {Http} from "@angular/http";

@Component({
    selector: 'user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

    avatarPath: string;
    user : User;
    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.avatarPath = 'assets/avatar.png'
        http.get(baseUrl+'api/user/dupa1234').subscribe(result=> this.user = result.json())
    }

    ngOnInit() {
    }

}

interface User{
    nickName:string;
    email:string;
    createTime:Date;
    points:number;
    votes: vote[];
    status: number;
}

interface vote{
    
}
