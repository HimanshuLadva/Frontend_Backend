import {AfterViewInit, Component, OnInit} from '@angular/core';
import {DynamicScriptLoaderService} from '../shared/service/dynamic-script-loader.service';

declare var $;

@Component({
  selector: 'app-core',
  templateUrl: './core.component.html',
  styleUrls: ['./core.component.scss']
})
export class CoreComponent implements OnInit, AfterViewInit {

  constructor(private dynamicScriptLoader: DynamicScriptLoaderService) {
  }

  ngOnInit(): void {
  }

  async ngAfterViewInit(): Promise<void> {
    try {
      await this.dynamicScriptLoader.load('main-js');
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

}
