import {Component, OnInit, ViewChild} from '@angular/core';
import {Image} from '../../modals/image.modal';
import {HomeGeneralService} from '../../services/home/home-general.service';
import { Router } from '@angular/router';
import {JsLoader} from '../../shared/static/js-loader';

declare var $;

@Component({
  selector: 'app-blogs',
  templateUrl: './blogs.component.html',
  styleUrls: ['./blogs.component.scss']
})
export class BlogsComponent implements OnInit {

  @ViewChild('refObj') ref;
  blogList: Image[];
  isloading = true;

  constructor(private homeGeneralService: HomeGeneralService, private router: Router) {
  }

  async ngOnInit(): Promise<void> {
    try {
      const res = await this.homeGeneralService.getBlogListing();
      this.blogList = res.data;
      this.isloading = false;
      setTimeout(() => {
        JsLoader.loadBlogJs(this.ref?.nativeElement);
      }, 10);
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  navigateToDetailPage(id:number) { 
     this.router.navigate(['blog-detail', id]);
  }
}
