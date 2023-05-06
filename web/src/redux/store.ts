import {configureStore} from "@reduxjs/toolkit";

import {IStore} from "../types/store.interfaces";

import {authReducer} from "./slices/auth/auth";


const store = configureStore<IStore>({
  reducer: {
    auth: authReducer,
  },
  devTools: true, 
});

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch
export default store ;
