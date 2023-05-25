import { UserProfile } from "./UserProfile";

export enum Role {
  user,
  admin,
}

export interface User {
  id?: number;
  name: string;
  password: string;

  role?: Role;
  userProfile?: UserProfile;
}
