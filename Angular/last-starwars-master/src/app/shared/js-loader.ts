declare var $: any;

export class JsLoader {
  public static sliderJs() {
    const js = document.createElement('script');
    js.innerHTML = `var swiper = new Swiper(".slide-content", {
      slidesPerView: 3,
      spaceBetween: 25,
      loop: true,
      centerSlide: 'true',
      fade: 'true',
      grabCursor: 'true',
      pagination: {
        el: ".swiper-pagination",
        clickable: true,
        dynamicBullets: true,
      },
      navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
      },
  
      breakpoints:{
          0: {
              slidesPerView: 1,
          },
          520: {
              slidesPerView: 2,
          },
          950: {
              slidesPerView: 3,
          },
          1380: {
              slidesPerView: 4,
          },
          1810: {
              slidesPerView: 5,
          },
      },
    });`;
    document.body.appendChild(js);
  }
}
