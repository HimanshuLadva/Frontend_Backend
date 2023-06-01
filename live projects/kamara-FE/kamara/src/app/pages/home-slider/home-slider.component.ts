import {
  AfterContentChecked,
  Component,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Image } from '../../modals/image.modal';
import { HomeGeneralService } from '../../services/home/home-general.service';
import { JsLoader } from '../../shared/static/js-loader';
import { EventBusEmitService } from '@shared/service/event-bus-emit.service';

declare var $;

@Component({
  selector: 'app-home-slider',
  templateUrl: './home-slider.component.html',
  styleUrls: ['./home-slider.component.scss'],
})
export class HomeSliderComponent implements OnInit, AfterContentChecked {
  @ViewChild('divRef') ref;
  loading = true;
  sliderList: Image[];

  constructor(
    private homeGeneralService: HomeGeneralService,
    private eventBusEmitService: EventBusEmitService
  ) {}

  async ngOnInit(): Promise<void> {
    try {
      const res = await this.homeGeneralService.getSliderListing();
      this.sliderList = res.data;
      this.loading = false;
      setTimeout(() => {
        JsLoader.loadHomeSliderJs(this.ref?.nativeElement);
        this.eventBusEmitService.showFooter();
      }, 10);
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  ngAfterContentChecked(): void {}

  // loadJsForBgImage():void{
  //   // Background Image JS start
  //   var bgSelector = $(".bg-img");
  //   bgSelector.each(function (index, elem) {
  //     var element = $(elem),
  //       bgSource = element.data('bg');
  //     element.css('background-image', 'url(' + bgSource + ')');
  //   });
  // }
}
