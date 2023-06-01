import { createReducer, on } from "@ngrx/store";
import { retrievedBookList } from "./books.actions";
import { Book } from "../book-list/books.model";

export const initailState: ReadonlyArray<Book> = [];

export const booksReducer = createReducer(
    initailState,
    on(retrievedBookList, (state, { books }) => books)
);
