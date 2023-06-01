import { Component, OnInit } from '@angular/core';
import { HomeGeneralService } from '@services/home/home-general.service';
import { ActivatedRoute } from '@angular/router';
import { Image } from '@modals/image.modal';

@Component({
  selector: 'app-blog-detail',
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.scss']
})
export class BlogDetailComponent implements OnInit {

  constructor(private homeGeneralService: HomeGeneralService ,private activatedRoute: ActivatedRoute) { }

  imageUrl:string;
  title:string;
  async ngOnInit(): Promise<void> {
    const res = await this.homeGeneralService.getBlogListing();
    this.imageUrl = res.data.filter(ele => ele.id == this.activatedRoute.snapshot.params.id)[0].image_url;
    this.title = res.data.filter(ele => ele.id == this.activatedRoute.snapshot.params.id)[0].title;
  }
}
