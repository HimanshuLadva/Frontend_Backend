import { Component, OnInit } from '@angular/core';
import { HomeGeneralService } from '../../services/home/home-general.service';
import { HomepageType } from '../../modals/homepage.modal';
import { productSliderType } from '@modals/product.modal';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  homePageStructure: HomepageType[];
  productSliderType = productSliderType;

  constructor(private homeGeneralService: HomeGeneralService) {}

  async ngOnInit(): Promise<void> {
    try {
      const res = await this.homeGeneralService.getHomePageStructure();

      res.data.push("reviews");
      this.homePageStructure = res.data;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }
}
