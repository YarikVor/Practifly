import {IStore} from "../interfaces/store.interfaces";

export const selectIsAuth = (state: IStore) => Boolean(state.auth.data);
