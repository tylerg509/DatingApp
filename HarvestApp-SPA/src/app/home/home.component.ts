import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  aboutMode = false;

  constructor(public authService: AuthService, private http: HttpClient) { }

  ngOnInit() {
  }

  registerToggle() {
    this.registerMode = true;
  }

  loggedIn() {
    return this.authService.loggedIn();

  }

  aboutToggle() {
    this.aboutMode = true;
  }

  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }

  cancelAboutMode(aboutMode: boolean) {
    this.aboutMode = aboutMode;
  }


}
