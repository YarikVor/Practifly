import {BaseResponse} from "../../interfaces/response.interface";

export interface IAuthData {
    token?: string;
}

export interface IUserRegisterData {
    username: string;
    name: string;
    surname: string;
    email: string;
    phone: string;
    birthday: string;
    password: string
}

export interface IUserLoginData {
    email: string;
    password: string;
}

export interface AuthStateProps{
    data?: BaseResponse<IAuthData> | null;
    status: string;
}