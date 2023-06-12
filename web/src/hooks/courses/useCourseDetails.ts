import {useEffect} from "react";

import {useSelector} from "react-redux";

import {CourseDetails} from "../../types/course.interface";
import {useAppDispatch} from "../hooks";
import {getCourseDetailsById} from "../../redux/slices/course/course.slice";
import {IStore} from "../../types/store.interfaces";
import {statusTypes} from "../../types/enums";

export const useCourseDetails = (id: number): [CourseDetails | null, boolean] => {
  const dispatch = useAppDispatch();
  const courseDetails = useSelector((store: IStore) => store.course.course.data);
  const isCourseDetailsLoading = useSelector((store: IStore) => store.course.course.status === statusTypes.LOADING);
  useEffect(() => {
    dispatch(getCourseDetailsById(id));
  }, [id]);
  return [courseDetails, isCourseDetailsLoading];
};