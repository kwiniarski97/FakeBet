import {Component} from '@angular/core';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    IsUserLogged: boolean = false;

    constructor() {
        let localStor = this.getLocalStorage();
        if (localStor) {
            if (localStor.getItem('currentUser')) {
                this.IsUserLogged = true;
            }
            else {
                this.IsUserLogged = false;
            }
        }
    }

    ngOnInit() {

    }

    getLocalStorage() {
        return (typeof window !== "undefined") ? window.localStorage : null;
    }
}
