import { Coverage } from "./Coverage";

export interface Course {
  id: string;
  name: string;
  description: string;
  contentUrl: string;
  coverage: Coverage;
  subjectId: string;
  index: number;
  isVisibleForStudents: boolean;
}
