export interface Coverage{
    id: string,
    percentage: number;
    badWords: string[];
    goodWords: string[];
}

export interface CoverageDetails extends Coverage {
    courseId: string
}