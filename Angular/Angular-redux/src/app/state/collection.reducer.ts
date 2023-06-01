import { createReducer, on } from "@ngrx/store";
import { addBook, removeBook } from "./books.actions";

export const initailState: ReadonlyArray<string> = [];

export const collectionReducer = createReducer(
    initailState,
    on(removeBook, (state , { bookId }) => state.filter((id) => id!== bookId)),
    on(addBook, (state , { bookId }) => {
        if(state.indexOf(bookId) > -1) return state;

        return [...state, bookId];
    })
)