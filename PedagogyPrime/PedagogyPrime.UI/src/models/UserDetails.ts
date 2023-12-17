export enum Role {
  NAR = "",
  Admin = "Admin",
  Teacher = "Teacher",
  Student = "Student",
}

const mapNumberToRole = (role: number): Role => {
  switch (role) {
    case 0:
      return Role.Admin;
    case 1:
      return Role.Teacher;
    case 2:
      return Role.Student;
    default:
      return Role.NAR;
  }
};

export const mapRoleToNumber = (role: Role): number => {
  switch (role) {
    case Role.Admin:
      return 0;
    case Role.Teacher:
      return 1;
    case Role.Student:
      return 2;
    default:
      return 2;
  }
};

export default mapNumberToRole;

export interface UserDetails {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  role: string;
}
