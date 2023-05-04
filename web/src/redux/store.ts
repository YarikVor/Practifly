import {combineReducers, configureStore} from "@reduxjs/toolkit";

import {useDispatch} from "react-redux";

import {authReducer} from "./slices/auth/auth";
import {IStore} from "./interfaces/store.interfaces";


const rootReducer = combineReducers({authReducer});
const store = configureStore<IStore>({
  reducer: {
    auth: authReducer,
  },
  devTools: true, 
});

export type RootState = ReturnType<typeof rootReducer>;
export const useAppDispatch = () => useDispatch<typeof store.dispatch>();
export default store ;
