import {useSelector} from "react-redux";

import {useLayoutEffect} from "react";

import {useAppDispatch} from "../hooks";
import {IStore} from "../../types/store.interfaces";
import {getMyCourses} from "../../redux/slices/course/course.slice";
import {statusTypes} from "../../types/enums";
import {Course} from "../../types/course.interface";

export const useMyCourses = (): [Course[], boolean] => {
  const dispatch = useAppDispatch();
  const myCourses = useSelector((state: IStore) => state.course.myCourses.data);
  const isLoading = useSelector((state: IStore) => state.course.myCourses.status === statusTypes.LOADING);

  useLayoutEffect(() => {
    dispatch(getMyCourses());
  }, []);
  return [myCourses, isLoading];
};