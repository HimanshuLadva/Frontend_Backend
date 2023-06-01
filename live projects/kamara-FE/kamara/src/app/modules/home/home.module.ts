import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { CollectionsComponent } from '../../pages/collections/collections.component';
import { TestimonialsComponent } from '../../pages/testimonials/testimonials.component';
import { BlogsComponent } from '../../pages/blogs/blogs.component';
import { OneImagePosterComponent } from '../../pages/one-image-poster/one-image-poster.component';
import { TwoImagePosterComponent } from '../../pages/two-image-poster/two-image-poster.component';
import { HomeSliderComponent } from '../../pages/home-slider/home-slider.component';
import { ProductSliderComponent } from '../../pages/product-slider/product-slider.component';
import { FollowUsComponent } from '../../pages/follow-us/follow-us.component';
import { ShowcaseComponent } from '../../pages/showcase/showcase.component';
import { SharedModule } from '../../shared/shared.module';
import { ReviewsComponent } from '@pages/reviews/reviews.component';

@NgModule({
  declarations: [
    HomeComponent,
    CollectionsComponent,
    TestimonialsComponent,
    BlogsComponent,
    OneImagePosterComponent,
    TwoImagePosterComponent,
    HomeSliderComponent,
    ProductSliderComponent,
    FollowUsComponent,
    ShowcaseComponent,
    ReviewsComponent
  ],
  exports: [HomeComponent, ShowcaseComponent, ProductSliderComponent],
  imports: [CommonModule, HomeRoutingModule, SharedModule],
})
export class HomeModule {}
