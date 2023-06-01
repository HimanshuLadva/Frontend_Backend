import { Component, OnInit, Input} from '@angular/core';

@Component({
  selector: 'app-chardetail',
  templateUrl: './chardetail.component.html',
  styleUrls: ['./chardetail.component.scss']
})
export class ChardetailComponent implements OnInit {

  constructor() { }

  // @Input() sendData:{name:string, height:string, mass:string}[]= [{name:'', height:'', mass:''}];
  // @Input() sendData:any;
  ngOnInit(): void {
    // console.log("data",this.sendData);
  }
}
