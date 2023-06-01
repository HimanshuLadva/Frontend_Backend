
import {Component, OnInit, ViewChild} from '@angular/core';
import {Image} from '../../modals/image.modal';
import {HomeGeneralService} from '../../services/home/home-general.service';
import { Router } from '@angular/router';
import {JsLoader} from '../../shared/static/js-loader';

@Component({
  selector: 'app-testimonials',
  templateUrl: './testimonials.component.html',
  styleUrls: ['./testimonials.component.scss']
})
export class TestimonialsComponent implements OnInit {

  @ViewChild('refObj') ref;
  testimonialsList: Image[];
  isLoading = true;

  constructor(private homeGeneralService: HomeGeneralService) {
  }

  async ngOnInit(): Promise<void> {
    try {
      const res = await this.homeGeneralService.getTestimonials();
      res.data.forEach((ele) => ele.content = ele.content.slice(3,-4));
      this.testimonialsList = res.data;
      this.isLoading = false;

      setTimeout(() => {
        JsLoader.loadTestimonialJs(this.ref?.nativeElement);
      }, 10);
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  plusSlides(n:number) {
    
  }

}
