import {createAsyncThunk, createSlice, SliceCaseReducers} from "@reduxjs/toolkit";

import axios from "../../../configure/axios";

import {statusTypes, endpointTypes} from "../../../types/enums";
import {
  AuthResponseData,
  PhotoURL,
  ProfileData, UpdateProfile, UserInitialState,
  UserLoginData,
  UserRegisterData,
} from "../../../types/user.interface";

export const fetchLogin = createAsyncThunk<AuthResponseData, UserLoginData, {rejectValue: string}>(
  "user/fetchLogin",
  async (loginData, thunkAPI) => {
    try {
      const {data} = await axios.post<AuthResponseData>(
        `${endpointTypes.USER}/login`,
        loginData
      );
      return data;
    } catch (e) {
      return thunkAPI.rejectWithValue("Something went wrong. Check please entered data");
    }
  }
);

export const updateProfile = createAsyncThunk<string, UpdateProfile, {rejectValue: string}>(
  "user/updateProfile",
  async (updateProfileData, thunkAPI) => {
    try {
      const {data} = await axios.post<string>(
        `${endpointTypes.USER}/profile/edit`,
        updateProfileData
      );
      return data;
    } catch (e) {
      return thunkAPI.rejectWithValue("Something went wrong. Check please entered data");
    }
  }
);

export const uploadPhoto = createAsyncThunk<PhotoURL, FormData, {rejectValue: string}>(
  "file/uploadPhoto",
  async (formData, thunkAPI) => {
    try {
      const {data} = await axios.post<PhotoURL>(
        `${endpointTypes.USER}/avatar`, formData,{
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
  "user/fetchRegistration",
  async (registrationData, thunkAPI) => {
    try {
      const {data} = await axios.post<AuthResponseData>(
        `${endpointTypes.USER}/register`,
        registrationData
      );
      return data;
    } catch (e) {
      return thunkAPI.rejectWithValue("Something went wrong. Check please entered data");
    }
  }
);

export const fetchMe = createAsyncThunk<ProfileData, void, {rejectValue: string}>(
  "user/fetchMe",
  async (_, thunkAPI) => {
    const response = await axios.get<ProfileData>(
      `${endpointTypes.USER}/profile/me`
    );
    if(!response) {
      return thunkAPI.rejectWithValue("Something went wrong. Check please entered data");
    }
    return response.data;
  }
);

const initialState: UserInitialState = {
  data: null,
  profileData: null,
  status: statusTypes.INIT,
  errorMessage: "",
};
const userSlice = createSlice<
    UserInitialState,
    SliceCaseReducers<UserInitialState>
>({
  name: "user",
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
    builder.addCase(uploadPhoto.fulfilled, (state) => {
      state.status = statusTypes.SUCCESS;
      state.errorMessage = null;
    });
    builder.addCase(uploadPhoto.rejected, (state, action) => {
      state.status = statusTypes.ERROR;
      state.errorMessage = action.payload;
    });
    builder.addCase(updateProfile.pending, (state) => {
      state.status = statusTypes.LOADING;
    });
    builder.addCase(updateProfile.fulfilled, (state) => {
      state.status = statusTypes.SUCCESS;
      state.errorMessage = null;
    });
    builder.addCase(updateProfile.rejected, (state, action) => {
      state.status = statusTypes.ERROR;
      state.errorMessage = action.payload;
    });
  }});
export const {reducer: userReducer} = userSlice;

