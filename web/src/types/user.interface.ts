import {statusTypes} from "./status.types";

export interface AuthResponseData  {
    token: string;
    user: UserData;

}

export interface UserData {
    birthday: string;
    email: string
    filePhoto: string
    firstName: string
    id: number
    lastName: string
    phoneNumber: string
    registrationDate: string
    username: string
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

export interface UserProfileData {
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
    data: AuthResponseData | null;
    status: statusTypes;
    errorMessage?: string | null;
}
