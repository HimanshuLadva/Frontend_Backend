import { Component, OnInit } from '@angular/core';
import {AuthService} from '@auth/service/auth.service';

@Component({
  selector: 'app-account-download-tab',
  templateUrl: './account-download-tab.component.html',
  styleUrls: ['./account-download-tab.component.scss']
})
export class AccountDownloadTabComponent implements OnInit {

  constructor(public authService: AuthService) {
  }

  ngOnInit(): void {
  }

}
