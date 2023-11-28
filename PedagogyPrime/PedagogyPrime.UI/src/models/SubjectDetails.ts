import { Course } from "./Course";

export interface SubjectDetails {
    id: string;
    name: string;
    period: string;
    noOfCourses: number;
}

export interface Subject extends SubjectDetails {
    coursesDetails: Course[];
}
