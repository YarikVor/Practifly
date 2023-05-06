import {createAsyncThunk, createSlice, SliceCaseReducers} from "@reduxjs/toolkit";


import axios from "../../../configure/axios";


import {statusTypes} from "../../../types/status.types";

import {AuthData, InitialState, UserLoginData, UserRegisterData} from "./auth.interfaces";

const authEndpoint = "/user";

export const fetchLogin = createAsyncThunk<AuthData, UserLoginData, {rejectValue: string}>(
  "auth/fetchLogin",
  async (loginData, thunkAPI) => {
    try {
      const response = await axios.post<AuthData>(
        `${authEndpoint}/login`,
        loginData
      );
      return response.data;
    } catch (e) {
      return thunkAPI.rejectWithValue("Something went wrong. Check please entered data");
    }
  }
);

export const fetchRegistration = createAsyncThunk<AuthData, UserRegisterData, {rejectValue: string}>(
  "auth/fetchRegistration",
  async (registrationData, thunkAPI) => {
    try {
      const response = await axios.post<AuthData>(
        `${authEndpoint}/registration`,
        registrationData
      );
      return response.data;
    } catch (e) {
      return thunkAPI.rejectWithValue("Something went wrong. Check please entered data");
    }
  }
);

const initialState: InitialState = {
  data: null,
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
      state.data = payload;
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
  }});
export const {reducer: authReducer} = authSlice;

