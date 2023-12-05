export interface Coverage{
    percentage: number;
    badWords: string[];
    goodWords: string[];
}

export interface CoverageDetails extends Coverage {
    courseId: string
}