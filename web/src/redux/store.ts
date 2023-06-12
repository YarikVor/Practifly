import {configureStore} from "@reduxjs/toolkit";

import {IStore} from "../types/store.interfaces";

import {userReducer} from "./slices/user/user.slice";
import {courseReducer} from "./slices/course/course.slice";


const store = configureStore<IStore>({
  reducer: {
    user: userReducer,
    course: courseReducer,
  },
  devTools: true, 
});

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch
export default store ;
