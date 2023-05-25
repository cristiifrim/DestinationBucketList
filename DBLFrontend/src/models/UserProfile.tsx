import { User } from "./User";

export interface UserProfile {
  id?: number;

  userId?: number;
  user?: User;

  email: string;
  birthday: Date;

  pagePreference?: number;
}
