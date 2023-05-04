import {
  createAsyncThunk,
  createSlice,
  SliceCaseReducers,
} from "@reduxjs/toolkit";

import {BaseResponse} from "../../interfaces/response.interface";

import axios from "../../../configure/axios";

import {AuthStateProps, IAuthData, IUserLoginData, IUserRegisterData} from "./auth.interfaces";


const authEndpoint = "/user";

export const fetchLogin = createAsyncThunk(
  "auth/fetchLogin",
  async (params: IUserLoginData) => {
    const {data} = await axios.post<BaseResponse<IAuthData>>(
      `${authEndpoint}/login`,
      params
    );
    return data;
  }
);

export const fetchRegister = createAsyncThunk(
  "auth/fetchRegister",
  async (params: IUserRegisterData) => {
    const { data } = await axios.post<BaseResponse<IAuthData>>(
      `${authEndpoint}/register`,
      params
    );
    return data;
  }
);

const authSlice = createSlice<
    AuthStateProps,
    SliceCaseReducers<AuthStateProps>
>({
  name: "auth",
  initialState: {
    data: null,
    status: "",
  },
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchLogin.pending, (state) => {
      state.status = "loading";
      state.data = null;
    });
    builder.addCase(fetchLogin.fulfilled, (state, {payload}) => {
      state.status = "loaded";
      state.data = payload;
    });
    builder.addCase(fetchLogin.rejected, (state) => {
      state.status = "error";
      state.data = null;
    });
  }});
export const authReducer = authSlice.reducer;

