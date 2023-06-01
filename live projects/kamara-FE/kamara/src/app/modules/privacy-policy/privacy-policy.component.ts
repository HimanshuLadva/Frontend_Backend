import { Component, OnInit } from '@angular/core';
import { SITE_NAME } from 'src/environments/environment';

@Component({
  selector: 'app-privacy-policy',
  templateUrl: './privacy-policy.component.html',
  styleUrls: ['./privacy-policy.component.scss']
})
export class PrivacyPolicyComponent implements OnInit {

  constructor() { }

  siteName = SITE_NAME;
  ngOnInit(): void {
  }

}
