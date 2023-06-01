import { FormGroup, FormControl } from '@angular/forms';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Autocomplete } from '@modals/autocomplete.modal';
import { RouteConfig } from '@shared/config/route-config';
import { SearchService } from '@shared/service/search.service';


declare var $;

@Component({
  selector: 'app-autocomplete',
  templateUrl: './autocomplete.component.html',
  styleUrls: ['./autocomplete.component.scss'],
})
export class AutocompleteComponent implements OnInit {
  @Input() isMobile = false;
  isOpen = false;

  @ViewChild('auto') auto;
  @ViewChild('input') input;
  keyword = 'meta_keywords';
  data: Autocomplete[] = [];
  searchKeyword;
  item;
  empty;

  constructor(private searchService: SearchService, private router: Router) {}

  ngOnInit(): void {
    // setTimeout(() => {
    //   this.loadJS();
    // }, 500);
  }

  async selectEvent(item): Promise<void> {
    this.item = item;
    this.formSubmit();
  }

  async onChangeSearch(val: string): Promise<void> {
    val = val.trim();
    this.isOpen = true;
    this.searchKeyword = val;
    if (val == '') {
      this.data = [];
      return;
    }
    try {
      const res = await this.searchService.search(val);
      if (res.code == 200) {
        this.data = res.data;
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  closed($event: void): void {}

  // formSubmit(): void {
  //   this.isOpen = false;
  //   if (this.item) {
  //     if (this.item.isCategory) {
  //       const url = this.item.url.split('/');
  //       this.router
  //         .navigate([RouteConfig.list, url[url.length - 1]])
  //         .then(() => {
  //           this.searchKeyword = '';
  //         });
  //     }
  //     if (this.item.product_id) {
  //       this.router
  //         .navigate([RouteConfig.productDetail, this.item.url])
  //         .then(() => {
  //           this.searchKeyword = '';
  //         });
  //     }
  //     this.auto.clear();
  //     this.auto.close();
  //     this.item = null;
  //     return;
  //   }
  //   if (this.searchKeyword == '') {
  //     return;
  //   }
  //   this.router
  //     .navigate([RouteConfig.searchProduct, this.searchKeyword])
  //     .then(() => {
  //       this.empty = '';
  //     });
  //   this.auto.clear();
  //   this.auto.close();
  // }

  searchProduct(serachValue: string) {
    this.input?.nativeElement.focus();
    var inputSearch = this.input?.nativeElement.value;
    if (inputSearch) {
      this.router
        .navigate([RouteConfig.searchProduct, inputSearch])
        .then(() => {
          this.input.nativeElement.value = '';
        });
    }
    
  }

  formSubmit() {
    var inputSearch = this.auto.nativeElement.value;
    if (inputSearch) {
      this.router
        .navigate([RouteConfig.searchProduct, inputSearch])
        .then(() => {
          this.auto.nativeElement.value = '';
        });
    }
  }

  getCategory(url): string {
    let x = url.split('/');
    return x.length == 2
      ? x[x.length - 2].replace('-', ' ')
      : x[0].replace('-', ' ');
  }

  onFocused(e): void {
    this.data = [];
  }

  closeEvent(): void {
    if (this.searchKeyword == '') {
      this.isOpen = false;
    }
  }

  // loadJS(): void {
  //   $('body').on('click', 'div.thirteen button.btn-search', function (event) {
  //     event.preventDefault();
  //     var $input = $('div.thirteen input');
  //     $input.focus();
  //     if ($input) {
  //       // submit form
  //     }
  //   });
  // }
}
