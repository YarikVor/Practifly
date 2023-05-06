import {IStore} from "../../types/store.interfaces";

export const selectIsAuth = (state: IStore) => Boolean(state.auth.data);
export const selectIsError = (state: IStore) => Boolean(state.auth.status === "error");
