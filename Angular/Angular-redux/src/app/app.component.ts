import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectBookCollection, selectBooks } from './state/books.selectors';
import { retrievedBookList, addBook, removeBook } from './state/books.actions';
import { BooksService } from './book-list/books.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Angular-redux';
  books$ = this.store.select(selectBooks);
  bookCollection$ = this.store.select(selectBookCollection);

  onAdd(bookId: string) {
    this.store.dispatch(addBook({bookId}));
  }

  onRemove(bookId: string) {
    this.store.dispatch(removeBook({bookId}));
  }

  constructor(private bookService: BooksService, private store: Store) {}

  ngOnInit():void {
    this.bookService.getBook().subscribe((books) => this.store.dispatch(retrievedBookList({books})));
  }
}
