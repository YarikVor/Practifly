import {statusTypes} from "../../../types/status.types";

export interface AuthData  {
    token: string;
}

export interface UserRegisterData {
    username: string;
    firstname: string;
    lastname: string;
    email: string;
    phoneNumber: string;
    birthday: string;
    password: string
}

export interface UserLoginData {
    email: string;
    password: string;
}

export interface InitialState {
    data: AuthData | null;
    status: statusTypes;
    errorMessage?: string | null;
}

export interface ErrorData {
    response:ResponseError;
}

export interface ResponseError {
status: number
}
