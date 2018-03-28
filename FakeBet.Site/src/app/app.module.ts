import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';


import {AppComponent} from './app.component';
import {AlertComponent} from './components/alert/alert.component';
import {SignInComponent} from './components/signin/signin.component';
import {LogoutComponent} from './components/logout/logout.component';
import {MatchesComponent} from './components/matches/matches.component';
import {NavbarComponent} from './components/navbar/navbar.component';
import {SignupComponent} from './components/signup/signup.component';
import {UserComponent} from './components/user/user.component';
import {AlertService} from './services/alert.service';
import {AuthenticationService} from './services/authentication.service';
import {MatchService} from './services/match.service';
import {UserService} from './services/user.service';
import {FormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import {AuthGuard} from './guards/auth.guard';
import {AppConfig} from './app-config';
import {HttpModule} from '@angular/http';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {DateTimeHelper} from './helpers/datetimehelper';
import {LocalStorageService} from './services/localstorage.service';
import {ChangePasswordComponent} from './components/changepassword/changepassword.component';
import {ChangeEmailComponent} from './components/changeemail/changeemail.component';
import {DeleteAccountComponent} from './components/deleteaccount/deleteaccount.component';
import {BetService} from './services/bet.service';
import { LoggedDirective } from './directives/logged.directive';


@NgModule({
  declarations: [
    AppComponent,
    AlertComponent,
    SignInComponent,
    LogoutComponent,
    MatchesComponent,
    NavbarComponent,
    SignupComponent,
    UserComponent,
    ChangePasswordComponent,
    ChangeEmailComponent,
    DeleteAccountComponent,
    LoggedDirective,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    NgbModule.forRoot(),
    RouterModule.forRoot([
      {path: '', redirectTo: 'home', pathMatch: 'full'},
      {path: 'home', component: MatchesComponent},
      {path: 'user', component: UserComponent, canActivate: [AuthGuard]},
      {path: 'user/change-email', component: ChangeEmailComponent},
      {path: 'user/change-password', component: ChangePasswordComponent},
      {path: 'user/delete', component: DeleteAccountComponent},
      {path: 'signin', component: SignInComponent},
      {path: 'logout', component: LogoutComponent, canActivate: [AuthGuard]},
      {path: 'signup', component: SignupComponent},
      {path: '**', redirectTo: 'home'}])
  ],
  providers: [
    AppConfig,
    AuthGuard,
    AlertService,
    AuthenticationService,
    MatchService,
    UserService,
    BetService,
    DateTimeHelper,
    LocalStorageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
