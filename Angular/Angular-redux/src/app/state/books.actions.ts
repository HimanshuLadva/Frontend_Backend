import { createAction, props } from "@ngrx/store";
import { Book } from "../book-list/books.model";

export const addBook = createAction(
    'Add Book',
    props<{ bookId: string }>()
);

export const removeBook = createAction(
    'Remove Book',
    props<{ bookId: string }>()
);

export const retrievedBookList = createAction(
    'Retrieve Books Success',
    props<{ books: ReadonlyArray<Book> }>()
);
