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
    countCompleted: number
    countInProgress: number
    averageGrade: number
    id: number
    username: string
    firstName: string
    lastName: string
    phoneNumber: string
    email: string
    birthday: string
    filePhoto: string
    registrationDate: string
}

export interface UserLoginData {
    email: string;
    password: string;
}

export interface ProfileData {
    countCompleted: number
    countInProgress: number
    averageGrade: number
    id: number
    username: string
    firstName: string
    lastName: string
    phoneNumber: string
    email: string
    birthday: string
    filePhoto: string
    registrationDate: string
}


export interface InitialState {
    data: AuthResponseData | null;
    profileData: ProfileData | null;
    status: statusTypes;
    errorMessage?: string | null;
}
