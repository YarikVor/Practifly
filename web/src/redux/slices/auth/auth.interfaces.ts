import {statusTypes} from "../../../types/status.types";

export interface AuthData  {
    token: string;
}

export interface UserRegisterData {
    username: string;
    name: string;
    surname: string;
    email: string;
    phone: string;
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
