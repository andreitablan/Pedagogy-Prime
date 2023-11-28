export enum Role {
  NAR = "",
  Admin = "Admin",
  Teacher = "Teacher",
  Student = "Student",
}

const mapToRole = (role: number): Role => {
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

export default mapToRole;

export interface UserDetails {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  role: string;
}
