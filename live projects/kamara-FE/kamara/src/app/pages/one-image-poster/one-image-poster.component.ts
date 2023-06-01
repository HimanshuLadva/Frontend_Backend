import { Component, OnInit } from '@angular/core';
import { HomeGeneralService } from '../../services/home/home-general.service';
import { Image } from '../../modals/image.modal';

@Component({
  selector: 'app-one-image-poster',
  templateUrl: './one-image-poster.component.html',
  styleUrls: ['./one-image-poster.component.scss'],
})
export class OneImagePosterComponent implements OnInit {
  loading = true;
  posterList: Image[];

  constructor(private homeGeneralService: HomeGeneralService) {}

  async ngOnInit(): Promise<void> {
    try {
      const res = await this.homeGeneralService.get1ImagesListing();
      this.posterList = res.data;
      this.loading = false;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }
}
