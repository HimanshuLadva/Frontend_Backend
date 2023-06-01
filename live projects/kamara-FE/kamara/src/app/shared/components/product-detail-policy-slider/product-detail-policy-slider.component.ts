import { Component, OnInit, ViewChild } from '@angular/core';
import { Image } from '@modals/image.modal';
import { HomeGeneralService } from '@services/home/home-general.service';
import { JsLoader } from '@shared/static/js-loader';

@Component({
  selector: 'app-product-detail-policy-slider',
  templateUrl: './product-detail-policy-slider.component.html',
  styleUrls: ['./product-detail-policy-slider.component.scss'],
})
export class ProductDetailPolicySliderComponent implements OnInit {
  @ViewChild('divRef') ref;

  loading = true;
  policyList: Image[];

  constructor(private homeGeneralService: HomeGeneralService) {}

  async ngOnInit(): Promise<void> {
    try {
      const res = await this.homeGeneralService.getShowcaseListing();
      this.policyList = res.data;
      this.loading = false;
      setTimeout(() => {
        JsLoader.loadShowcaseSliderJs(this.ref.nativeElement);
      }, 10);
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }
}
