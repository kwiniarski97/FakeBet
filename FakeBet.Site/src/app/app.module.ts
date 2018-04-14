import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';


import {AppComponent} from './app.component';
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
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import {AuthGuard} from './guards/auth.guard';
import {AppConfig} from './app-config';
import {HttpModule} from '@angular/http';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {LocalStorageService} from './services/localstorage.service';
import {ChangePasswordComponent} from './components/changepassword/changepassword.component';
import {ChangeEmailComponent} from './components/changeemail/changeemail.component';
import {DeleteAccountComponent} from './components/deleteaccount/deleteaccount.component';
import {BetService} from './services/bet.service';
import {LoggedDirective} from './directives/logged.directive';
import {HighscoreesComponent} from './components/highscorees/highscorees.component';
import {AdminMatchComponent} from './components/admin/admin-match/admin-match.component';
import {AdminUserComponent} from './components/admin/admin-user/admin-user.component';
import {CalendarModule} from 'primeng/primeng';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {CountryCodesService} from './services/country-codes.service';


@NgModule({
  declarations: [
    AppComponent,
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
    HighscoreesComponent,
    AdminMatchComponent,
    AdminUserComponent,

  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    CalendarModule,
    BrowserAnimationsModule,
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
      {path: 'highscores', component: HighscoreesComponent},
      {path: 'admin/matches', component: AdminMatchComponent},
      {path: 'admin/users', component: AdminUserComponent},
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
    LocalStorageService,
    CountryCodesService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
