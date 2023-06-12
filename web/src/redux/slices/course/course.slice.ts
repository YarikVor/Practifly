import {createAction, createAsyncThunk, createSlice, SliceCaseReducers} from "@reduxjs/toolkit";

import {statusTypes} from "../../../types/enums";
import {Course, CourseDetails, CourseInitialState, Material, MaterialStatus} from "../../../types/course.interface";
import axios from "../../../configure/axios";

export const getMyCourses = createAsyncThunk<Course[], void, {rejectValue: string}>(
  "courses/myCourses",
  async (_, thunkAPI) => {
    try {
      const {data} = await axios.get<Course[]>(
        "user/my/courses"
      );
      return data;
    } catch (e) {
      return thunkAPI.rejectWithValue("Something went wrong");
    }
  }
);

export const getCourseDetailsById = createAsyncThunk<CourseDetails, number, {rejectValue: string}>(
  "course/fullInfoById",
  async (id, thunkAPI) => {
    try {
      const {data} = await axios.get<CourseDetails>(
        `course/themes/full-info?courseId=${id}`,
      );
      return data;
    } catch (e) {
      return thunkAPI.rejectWithValue("Something went wrong");
    }
  }
);

export const changeMaterialStatus = createAsyncThunk<void, MaterialStatus, {rejectValue: string}>(
  "course/fullInfoById",
  async (materialStatus, thunkAPI) => {
    try {
      const {data} = await axios.post(
        "user/material/status",
        materialStatus
      );
      return data;
    } catch (e) {
      return thunkAPI.rejectWithValue("Something went wrong");
    }
  }
);

export const getCurrentMaterials = createAction(
  "material/current",
  (currentMaterial: Material) => {
    return { payload: currentMaterial };
  }
);

const initialState: CourseInitialState = {
  myCourses: {
    data: [],
    status: statusTypes.INIT,
    errorMessage: "",
  },
  course: {
    data: null,
    status: statusTypes.INIT,
    errorMessage: "",
  },
  currentMaterials: {
    data:null,
    status: statusTypes.INIT,
    errorMessage: "",
  },
};
const courseSlice = createSlice<
    CourseInitialState,
    SliceCaseReducers<CourseInitialState>
>({
  name: "course",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(getCurrentMaterials, (state, action) => {
      state.currentMaterials.status = statusTypes.SUCCESS;
      state.currentMaterials.data = action.payload;
    });
    builder.addCase(getMyCourses.pending, (state) => {
      state.myCourses.status = statusTypes.LOADING;
      state.myCourses.data = [];
    });
    builder.addCase(getMyCourses.fulfilled, (state, {payload}) => {
      state.myCourses.status = statusTypes.SUCCESS;
      state.myCourses.data = payload;
      state.myCourses.errorMessage = null;
    });
    builder.addCase(getMyCourses.rejected, (state, action) => {
      state.myCourses.status = statusTypes.ERROR;
      state.myCourses.errorMessage = action.payload;
    });
    builder.addCase(getCourseDetailsById.pending, (state) => {
      state.course.status = statusTypes.LOADING;
      state.course.data = null;
    });
    builder.addCase(getCourseDetailsById.fulfilled, (state, {payload}) => {
      state.course.status = statusTypes.SUCCESS;
      state.course.data = payload;
      state.course.errorMessage = null;
    });
    builder.addCase(getCourseDetailsById.rejected, (state, action) => {
      state.course.status = statusTypes.ERROR;
      state.course.errorMessage = action.payload;
    });
  },
});

export const {reducer: courseReducer} = courseSlice;
