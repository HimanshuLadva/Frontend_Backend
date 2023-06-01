import { Component, OnInit, ViewChild } from '@angular/core';
import { Image } from '../../modals/image.modal';
import { HomeGeneralService } from '../../services/home/home-general.service';
import { JsLoader } from '../../shared/static/js-loader';

declare var $;

@Component({
  selector: 'app-showcase',
  templateUrl: './showcase.component.html',
  styleUrls: ['./showcase.component.scss'],
})
export class ShowcaseComponent implements OnInit {
  @ViewChild('divRef') ref;

  loading = true;
  showcaseList: Image[];

  constructor(private homeGeneralService: HomeGeneralService) {}

  async ngOnInit(): Promise<void> {
    try {
      const res = await this.homeGeneralService.getShowcaseListing();
      this.showcaseList = res.data;
      this.loading = false;
      setTimeout(() => {
        JsLoader.loadShowcaseSliderJs(this.ref?.nativeElement);
      }, 10);
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }
}
