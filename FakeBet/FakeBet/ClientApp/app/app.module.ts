import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './components/app/app.component';
import {NavMenuComponent} from './components/navmenu/navmenu.component';
import {HomeComponent} from './components/home/home.component';
import {UserComponent} from "./components/user/user.component";
import {LoginComponent} from "./components/login/login.component";
import {AppConfig} from "./app-config";
import {AuthGuard} from "./guards/auth.guard";
import {AuthenticationService} from "./services/authentication.service";
import {RegisterComponent} from "./components/register/register.component";
import {LogoutComponent} from "./components/logout/logout.component";
import {UserService} from "./services/user.service";
import {AlertService} from "./services/alert.service";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        UserComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent,
        LogoutComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            {path: '', redirectTo: 'home', pathMatch: 'full'},
            {path: 'home', component: HomeComponent},
            {path: 'user', component: UserComponent, canActivate: [AuthGuard]},
            {path: 'login', component: LoginComponent},
            {path: 'logout', component: LogoutComponent},
            {path: 'register', component: RegisterComponent},
            {path: '**', redirectTo: 'home'}
        ])
    ],
    providers: [
        AppConfig,
        AuthGuard,
        AuthenticationService,
        AlertService,
        UserService
    ]
})
export class AppModuleShared {
}
