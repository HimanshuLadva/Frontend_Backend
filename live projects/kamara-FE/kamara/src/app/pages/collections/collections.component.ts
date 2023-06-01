import {Component, OnInit} from '@angular/core';
import {Collection} from 'src/app/modals/collection.modal';
import {HomeGeneralService} from '../../services/home/home-general.service';

@Component({
  selector: 'app-collections',
  templateUrl: './collections.component.html',
  styleUrls: ['./collections.component.scss']
})
export class CollectionsComponent implements OnInit {

  loading = true;
  collectionList: Collection[] = [];

  constructor(private homeGeneralService: HomeGeneralService) {
  }

  async ngOnInit(): Promise<void> {
    try {
      const res = await this.homeGeneralService.getCollectionListing();
      this.collectionList = res.data;
      this.loading = false;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

}
