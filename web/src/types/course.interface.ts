import {getCurrentMaterials} from "../redux/slices/course/course.slice";

import {statusTypes} from "./enums";

export interface CourseInitialState {
    myCourses: {
        data: Course[] | [];
        status: statusTypes;
        errorMessage?: null | string;
    }
    course: {
        data: CourseDetails | null;
        status: statusTypes;
        errorMessage?: null | string;
    }
    currentMaterials: {
        data: Material | null;
        status: statusTypes;
        errorMessage?: null | string;
    }
}
export interface CourseDetails {
    id: number
    name: string
    themes: Theme[]
}

export interface MaterialStatus {
    materialId: number,
    resultUrl: string
    isCompleted: boolean
}

export interface Theme {
    id: number
    name: string
    isCompleted: boolean
    grade: number
    materials: Material[]
}

export interface Material {
    id: number
    name: string
    grade: number
    note: string
    isCompleted: boolean
    url:string
}


export interface Course {
    courseId: number
    language: string
    name: string
    countProgress: number
    countCompleted: number
    countThemes: number
    isCompleted: boolean
    isChecked: boolean
    description: string
    grade: number
    gradeAverage: number
    themeId: any
}