import {UserInitialState} from "./user.interface";
import {CourseInitialState} from "./course.interface";

export interface IStore {
    user: UserInitialState,
    course: CourseInitialState,
}