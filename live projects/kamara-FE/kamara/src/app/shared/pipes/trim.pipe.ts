import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'trim'
})
export class TrimPipe implements PipeTransform {
  transform(value: string, limit: number): string {
    if (!value || value.length < 0) {
      return '';
    }
    if (limit < 3) {
      return '...';
    }
    return value.length >= limit ? value.slice(0, limit - 3) + '...' : value;
  }
}
