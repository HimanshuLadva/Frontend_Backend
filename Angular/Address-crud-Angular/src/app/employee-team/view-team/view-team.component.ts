import { Component, OnInit } from '@angular/core';
import { Team } from '../team';
import { TeamdataService } from '../teamdata.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-team',
  templateUrl: './view-team.component.html',
  styleUrls: ['./view-team.component.scss']
})
export class ViewTeamComponent implements OnInit {

  constructor(private teamDataService: TeamdataService, private router: Router) { }

  teamList:Team[] = [];
  ngOnInit(): void {
    this.teamList = this.teamDataService.teamArray;
  }
  
  moveToAdd() {
    this.router.navigate(['teamemployee/add']);
  }

  editTeam(id:number) {
    console.log("this is id", id);
    this.teamDataService.getTeamDataInService(id);
    this.router.navigate(['teamemployee/edit', id]);
  }

  deleteTeam(id:number) {
    this.teamDataService.deleteDataInLocalStorage(id);
    
    this.ngOnInit();
  }
}
