import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  userId: string | null = null;
  constructor(private route:ActivatedRoute) {
   
  }
  
  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      console.log("id", typeof params["id"]);
      if(params["id"]) {
        this.userId = params["id"].slice(0,2);
      }
    });
  }
}
