import { Component, OnInit } from '@angular/core';
import {AuthService} from '@auth/service/auth.service';

@Component({
  selector: 'app-account-dashboard-tab',
  templateUrl: './account-dashboard-tab.component.html',
  styleUrls: ['./account-dashboard-tab.component.scss']
})
export class AccountDashboardTabComponent implements OnInit {

  constructor(public authService: AuthService) {
  }

  ngOnInit(): void {
  }

}
