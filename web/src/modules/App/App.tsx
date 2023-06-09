
import {RouterProvider} from "react-router-dom";
import React, {useEffect} from "react";

import {router} from "../../Router/router";
import {useAppDispatch, useAppSelector} from "../../hooks/hooks";
import {fetchMe} from "../../redux/slices/user/user.slice";

export const App = () => {
  const dispatch = useAppDispatch();
  const isAuth = useAppSelector(store => Boolean(store.user.profileData));
  useEffect(() => {
    dispatch(fetchMe());
  },[]);
  return (
    <RouterProvider router={router(isAuth)}/>
  );
};