import {createAsyncThunk, createSlice, SliceCaseReducers} from "@reduxjs/toolkit";

import axios from "../../../configure/axios";


import {statusTypes} from "../../../types/status.types";
import {
  AuthResponseData,
  InitialState,
  ProfileData,
  UserLoginData,
  UserRegisterData,
} from "../../../types/user.interface";

const authEndpoint = "/user";

export const fetchLogin = createAsyncThunk<AuthResponseData, UserLoginData, {rejectValue: string}>(
  "auth/fetchLogin",
  async (loginData, thunkAPI) => {
    try {
      const {data} = await axios.post<AuthResponseData>(
        `${authEndpoint}/login`,
        loginData
      );
      console.log(data);
      return data;
    } catch (e) {
      return thunkAPI.rejectWithValue("Something went wrong. Check please entered data");
    }
  }
);

export const uploadPhoto = createAsyncThunk<string, FormData, {rejectValue: string}>(
  "file/uploadPhoto",
  async (formData, thunkAPI) => {
    try {
      const {data} = await axios.post<string>(
        "bucket/upload", formData,{
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );
      return data;
    } catch (e) {
      return thunkAPI.rejectWithValue("Something went wrong. Check please photo");
    }
  }
);

export const fetchRegistration = createAsyncThunk<AuthResponseData, UserRegisterData, {rejectValue: string}>(
  "auth/fetchRegistration",
  async (registrationData, thunkAPI) => {
    try {
      const {data} = await axios.post<AuthResponseData>(
        `${authEndpoint}/register`,
        registrationData
      );
      return data;
    } catch (e) {
      return thunkAPI.rejectWithValue("Something went wrong. Check please entered data");
    }
  }
);

export const fetchMe = createAsyncThunk<ProfileData, void, {rejectValue: string}>(
  "auth/fetchMe",
  async (_, thunkAPI) => {
    const response = await axios.get<ProfileData>(
      `${authEndpoint}/profile/me`
    );
    if(!response) {
      return thunkAPI.rejectWithValue("Something went wrong. Check please entered data");
    }
    return response.data;
  }
);

const initialState: InitialState = {
  data: null,
  profileData: null,
  status: statusTypes.INIT,
  errorMessage: "",
};
const authSlice = createSlice<
    InitialState,
    SliceCaseReducers<InitialState>
>({
  name: "auth",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchLogin.pending, (state) => {
      state.status = statusTypes.LOADING;
      state.data = null;
    });
    builder.addCase(fetchLogin.fulfilled, (state, {payload}) => {
      state.status = statusTypes.SUCCESS;
      state.data = {...payload};
      state.errorMessage = null;
    });
    builder.addCase(fetchLogin.rejected, (state, action) => {
      state.status = statusTypes.ERROR;
      state.errorMessage = action.payload;
    });
    builder.addCase(fetchRegistration.pending, (state) => {
      state.status = statusTypes.LOADING;
      state.data = null;
    });
    builder.addCase(fetchRegistration.fulfilled, (state, {payload}) => {
      state.status = statusTypes.SUCCESS;
      state.data = payload;
      state.errorMessage = null;
    });
    builder.addCase(fetchRegistration.rejected, (state, action) => {
      state.status = statusTypes.ERROR;
      state.errorMessage = action.payload;
    });
    builder.addCase(fetchMe.pending, (state) => {
      state.status = statusTypes.LOADING;
      state.profileData = null;
    });
    builder.addCase(fetchMe.fulfilled, (state, {payload}) => {
      state.status = statusTypes.SUCCESS;
      state.profileData = payload;
      state.errorMessage = null;
    });
    builder.addCase(fetchMe.rejected, (state, action) => {
      state.status = statusTypes.ERROR;
      state.errorMessage = action.payload;
    });
    builder.addCase(uploadPhoto.pending, (state) => {
      state.status = statusTypes.LOADING;
    });
    builder.addCase(uploadPhoto.fulfilled, (state, {payload}) => {
      state.status = statusTypes.SUCCESS;
      state.profileData!.filePhoto = payload;
      state.errorMessage = null;
    });
    builder.addCase(uploadPhoto.rejected, (state, action) => {
      state.status = statusTypes.ERROR;
      state.errorMessage = action.payload;
    });
  }});
export const {reducer: authReducer} = authSlice;

