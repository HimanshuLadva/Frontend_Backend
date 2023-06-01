import { Component, OnInit } from '@angular/core';
import {Image} from '../../modals/image.modal';
import {HomeGeneralService} from '../../services/home/home-general.service';

@Component({
  selector: 'app-two-image-poster',
  templateUrl: './two-image-poster.component.html',
  styleUrls: ['./two-image-poster.component.scss']
})
export class TwoImagePosterComponent implements OnInit {

  posterList: Image[];
  loading = true;

  constructor(private homeGeneralService: HomeGeneralService) {
  }

  async ngOnInit(): Promise<void> {
    try {
      const res = await this.homeGeneralService.get2ImagesListing();
      this.posterList = res.data;
      this.loading = false;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

}
