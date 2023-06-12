import {statusTypes} from "./enums";

export interface AuthResponseData  {
    token: string;
    user: UserData;

}

export interface PhotoURL{
    url: string;
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

export interface UserLoginData {
    email: string;
    password: string;
}

export interface ProfileData extends UserData{
    countCompleted: number
    countInProgress: number
    averageGrade: number
}

export type UpdateProfile = Omit<UserData, "id" | "filePhoto" | "registrationDate">;
export interface UserInitialState {
    data: AuthResponseData | null;
    profileData: ProfileData | null;
    status: statusTypes;
    errorMessage?: string | null;
}
