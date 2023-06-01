declare var $;
// $(`.${className}`)
// public static loadBlogJs(className = 'blog-carousel-active'): void{}
export class JsLoader {
  public static loadBlogJs(ref: any = '.blog-carousel-active'): void {
    try {
      // blog carousel active
      $(ref).slick({
        autoplay: true,
        speed: 1000,
        slidesToShow: 3,
        adaptiveHeight: true,
        prevArrow:
          '<button type="button" class="slick-prev"><i class="pe-7s-angle-left"></i></button>',
        nextArrow:
          '<button type="button" class="slick-next"><i class="pe-7s-angle-right"></i></button>',
        responsive: [
          {
            breakpoint: 992,
            settings: {
              slidesToShow: 2,
            },
          },
          {
            breakpoint: 768,
            settings: {
              arrows: false,
              slidesToShow: 1,
            },
          },
        ],
      });
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  // public static loadProductSliderJs(className: string = ''): void {
  // }

  public static loadTestimonialJs(
    ref1: any = '.testimonial-thumb-carousel',
    ref2: any = '.testimonial-content-carousel'
  ): void {
    try {
      // product details slider nav active
      $(ref1).slick({
        slidesToShow: 3,
        asNavFor: ref2,
        centerMode: true,
        arrows: true,
        centerPadding: 0,
        focusOnSelect: true,
      });

      // testimonial cariusel active js
      $(ref2).slick({
        arrows: false,
        asNavFor: ref1,
      });
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }
  public static loadReviewJs(
    ref2: any = '.review-content-carousel'
  ): void {
    try {
      // review cariusel active js
      $(ref2).slick({
        arrows: true,
      });
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  public static loadShowcaseSliderJs(ref: any = '.brand-logo-carousel'): void {
    try {
      // brand logo carousel active js
      $(ref).slick({
        speed: 1000,
        slidesToShow: 5,
        infinite:true,
        autoplay: true,
        adaptiveHeight: true,
        arrows:false,
        prevArrow:
          '<button type="button" class="slick-prev"><i class="pe-7s-angle-left"></i></button>',
        nextArrow:
          '<button type="button" class="slick-next"><i class="pe-7s-angle-right"></i></button>',
        responsive: [
          {
            breakpoint: 1200,
            settings: {
              slidesToShow: 4,
            },
          },
          {
            breakpoint: 992,
            settings: {
              slidesToShow: 3,
              arrows: false,
            },
          },
          {
            breakpoint: 768,
            settings: {
              slidesToShow: 2,
              arrows: false,
            },
          },
          {
            breakpoint: 480,
            settings: {
              slidesToShow: 2,
              arrows: false,
            },
          },
        ],
      });
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  public static loadHomeSliderJs(ref: any = '.hero-slider-active'): void {
    try {
      $(ref).slick({
        fade: true,
        speed: 1000,
        dots: false,
        autoplay: true,
        prevArrow:
          '<button type="button" class="slick-prev"><i class="pe-7s-angle-left"></i></button>',
        nextArrow:
          '<button type="button" class="slick-next"><i class="pe-7s-angle-right"></i></button>',
        responsive: [
          {
            breakpoint: 992,
            settings: {
              arrows: false,
              dots: true,
            },
          },
        ],
      });
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  public static loadProductSliderJs(ref: any = '.product-carousel-4'): void {
    try {
      $(ref).slick({
        speed: 1000,
        autoplay: true,
        slidesToShow: 5,
        adaptiveHeight: true,
        prevArrow:
          '<button type="button" class="slick-prev"><i class="pe-7s-angle-left"></i></button>',
        nextArrow:
          '<button type="button" class="slick-next"><i class="pe-7s-angle-right"></i></button>',
        responsive: [
          {
            breakpoint: 992,
            settings: {
              slidesToShow: 4,
            },
          },
          {
            breakpoint: 768,
            settings: {
              slidesToShow: 2,
              arrows: false,
            },
          },
          {
            breakpoint: 480,
            settings: {
              slidesToShow: 2,
              arrows: false,
            },
          },
        ],
      });

      // tooltip active js
      $('[data-toggle="tooltip"]').tooltip();
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }
  public static loadProductDetailSliderJs(
    ref: any = '.product-carousel-4'
  ): void {
    try {
      $(ref).slick({
        speed: 1000,
        autoplay: true,
        slidesToShow: 4,
        adaptiveHeight: true,
        prevArrow:
          '<button type="button" class="slick-prev"><i class="pe-7s-angle-left"></i></button>',
        nextArrow:
          '<button type="button" class="slick-next"><i class="pe-7s-angle-right"></i></button>',
        responsive: [
          {
            breakpoint: 992,
            settings: {
              slidesToShow: 3,
            },
          },
          {
            breakpoint: 768,
            settings: {
              slidesToShow: 2,
              arrows: false,
            },
          },
          {
            breakpoint: 480,
            settings: {
              slidesToShow: 2,
              arrows: false,
            },
          },
        ],
      });

      // tooltip active js
      $('[data-toggle="tooltip"]').tooltip();
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  public static hideMobileMenu(): void {
    $('body').removeClass('fix');
    $('.off-canvas-wrapper.off-canvas-wrapper-mega-menu').removeClass('open');
  }

  public static loadMobileMenuJs(): void {
    // Off Canvas Open close
    $('.mobile-menu-btn').on('click', function () {
      $('body').addClass('fix');
      $('.off-canvas-wrapper.off-canvas-wrapper-mega-menu').addClass('open');
    });

    $('.btn-close-off-canvas,.off-canvas-overlay').on('click', function () {
      $('body').removeClass('fix');
      $('.off-canvas-wrapper.off-canvas-wrapper-mega-menu').removeClass('open');
    });

    // offcanvas mobile menu
    var $offCanvasNav = $('.mobile-menu'),
      $offCanvasNavSubMenu = $offCanvasNav.find('.dropdown');

    /*Add Toggle Button With Off Canvas Sub Menu*/
    $offCanvasNavSubMenu
      .parent()
      .prepend('<span class="menu-expand"><i></i></span>');

    /*Close Off Canvas Sub Menu*/
    $offCanvasNavSubMenu.slideUp();

    /*Category Sub Menu Toggle*/
    $offCanvasNav.on('click', 'li a, li .menu-expand', function (e) {
      var $this = $(this);
      if (
        $this
          .parent()
          .attr('class')
          .match(/\b(menu-item-has-children|has-children|has-sub-menu)\b/) &&
        ($this.attr('href') === '#' || $this.hasClass('menu-expand'))
      ) {
        e.preventDefault();
        if ($this.siblings('ul:visible').length) {
          $this.parent('li').removeClass('active');
          $this.siblings('ul').slideUp();
        } else {
          $this.parent('li').addClass('active');
          $this
            .closest('li')
            .siblings('li')
            .removeClass('active')
            .find('li')
            .removeClass('active');
          $this.closest('li').siblings('li').find('ul:visible').slideUp();
          $this.siblings('ul').slideDown();
        }
      }
    });
  }

  public static loadTooltipActiveJs(): void {
    $('[data-toggle="tooltip"]').tooltip();
  }

  public static loadStickyMenuJs(): void {
    var $window = $(window);
    $window.on('scroll', function () {
      var scroll = $window.scrollTop();
      if (scroll < 300) {
        $('.sticky').removeClass('is-sticky');
      } else {
        $('.sticky').addClass('is-sticky');
      }
    });
  }

  public static loadProductQuickViewJs(): void {
    try {
      // prodct details slider active
      $('.product-large-slider').slick({
        fade: true,
        arrows: false,
        speed: 1000,
        asNavFor: '.pro-nav',
      });

      // product details slider nav active
      $('.pro-nav').slick({
        slidesToShow: 4,
        asNavFor: '.product-large-slider',
        centerMode: true,
        speed: 1000,
        centerPadding: 0,
        focusOnSelect: true,
        prevArrow:
          '<button type="button" class="slick-prev"><i class="lnr lnr-chevron-left"></i></button>',
        nextArrow:
          '<button type="button" class="slick-next"><i class="lnr lnr-chevron-right"></i></button>',
        responsive: [
          {
            breakpoint: 576,
            settings: {
              slidesToShow: 3,
            },
          },
        ],
      });
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  public static loadProductDetailJs(): void {
    try {
      const $product_large_slider_mobile = $('.product-large-slider-mobile');
      const $pro_nav_mobile = $('.pro-nav-mobile');
      const $product_large_slider_desktop = $('.product-large-slider-desktop');
      const $pro_nav_desktop = $('.pro-nav-desktop');
      // product details slider active
      if ($product_large_slider_mobile.hasClass('slick-initialized')) {
        $product_large_slider_mobile.slick('unslick');
      }
      if (!$product_large_slider_mobile.hasClass('slick-initialized')) {
        $product_large_slider_mobile.slick({
          fade: true,
          arrows: false,
          speed: 1000,
          asNavFor: '.pro-nav-mobile',
        });
      }

      // product details slider nav active
      if ($pro_nav_mobile.hasClass('slick-initialized')) {
        $pro_nav_mobile.slick('unslick');
      }
      if (!$pro_nav_mobile.hasClass('slick-initialized')) {
        $pro_nav_mobile.slick({
          slidesToShow: 4,
          slidesToScroll: 4,
          asNavFor: '.product-large-slider-mobile',
          centerMode: true,
          speed: 1000,
          centerPadding: 0,
          focusOnSelect: true,
          arrows: false,
          responsive: [
            {
              breakpoint: 576,
              settings: {
                slidesToShow: 3,
              },
            },
          ],
        });
      }

      // product details slider active
      if ($product_large_slider_desktop.hasClass('slick-initialized')) {
        $product_large_slider_desktop.slick('unslick');
      }
      if (!$product_large_slider_desktop.hasClass('slick-initialized')) {
        $product_large_slider_desktop.slick({
          fade: true,
          arrows: false,
          speed: 1000,
          asNavFor: '.pro-nav-desktop',
        });
      }

      // product details slider nav active
      if ($pro_nav_desktop.hasClass('slick-initialized')) {
        $pro_nav_desktop.slick('unslick');
      }
      if (!$pro_nav_desktop.hasClass('slick-initialized')) {
        $pro_nav_desktop.slick({
          slidesToShow: 4,
          slidesToScroll: 4,
          asNavFor: '.product-large-slider-desktop',
          centerMode: true,
          speed: 1000,
          centerPadding: 0,
          focusOnSelect: true,
          vertical: true,
          verticalSwiping: true,
          arrows: false,
        });
        $('.pro-nav-desktop .slick-list').height('400px');
      }
      $('.img-zoom').zoom();
    } catch (e) {
      console.error('::::product detail js::::', e);
    }
  }

  public static loadProductDetailPopUpJs(): void {
    try {
      const $product_large_slider_mobile = $(
        '.product-large-slider-mobile-popup'
      );
      const $pro_nav_mobile = $('.pro-nav-mobile-popup');

      // product details slider active
      if ($product_large_slider_mobile.hasClass('slick-initialized')) {
        $product_large_slider_mobile.slick('unslick');
      }
      if (!$product_large_slider_mobile.hasClass('slick-initialized')) {
        $product_large_slider_mobile.slick({
          fade: true,
          arrows: false,
          speed: 1000,
          autoplay: true,
          asNavFor: '.pro-nav-mobile-popup',
        });
      }

      // product details slider nav active
      if ($pro_nav_mobile.hasClass('slick-initialized')) {
        $pro_nav_mobile.slick('unslick');
      }
      if (!$pro_nav_mobile.hasClass('slick-initialized')) {
        $pro_nav_mobile.slick({
          slidesToShow: 4,
          slidesToScroll: 4,
          asNavFor: '.product-large-slider-mobile-popup',
          centerMode: true,
          speed: 1000,
          centerPadding: 0,
          focusOnSelect: true,
          arrows: false,
          responsive: [
            {
              breakpoint: 576,
              settings: {
                slidesToShow: 3,
              },
            },
          ],
        });
      }
      $('.img-zoom').zoom();
    } catch (e) {
      console.error('::::product detail js::::', e);
    }
  }

  public static loadTooltip(): void {
    try {
      // tooltip active js
      $('[data-toggle="tooltip"]').tooltip();
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }
}
