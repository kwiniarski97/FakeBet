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


@NgModule({
  declarations: [
    AppComponent,
    AlertComponent,
    SignInComponent,
    LogoutComponent,
    MatchesComponent,
    NavbarComponent,
    SignupComponent,
    UserComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot([
      {path: '', redirectTo: 'home', pathMatch: 'full'},
      {path: 'home', component: MatchesComponent},
      {path: 'user', component: UserComponent, canActivate: [AuthGuard]},
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
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
